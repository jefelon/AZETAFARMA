using SistemaVentas.Datos;
using SistemaVentas.Entidad;
using SistemaVentas.Presentacion.Clientes;
using SistemaVentas.Presentacion.Proveedores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastReport;
using SisttVentas;

namespace SistemaVentas.Presentacion.Compras
{
    public partial class FrmComprasAsistente : Form
    {
        public FrmComprasAsistente()
        {
            InitializeComponent();
        }

        //1c  instancia unica
        private static FrmComprasAsistente _instancia;
        public static FrmComprasAsistente InstanciaUnica()
        {
            if (_instancia == null)
                _instancia = new FrmComprasAsistente();
            return _instancia;
        }

        private static DataTable dt = new DataTable(); //variable para crear un tabla
        private static DataTable dtItems = new DataTable();//variable para crear un tabla para items de vent
        private static DataTable dtable = new DataTable();//variable para crear un tabla para los tipos de comprobante
        private void FrmComprasAsistente_Load(object sender, EventArgs e)
        {

            MostrarGuardarCancelar(false);
            productosImprimir(false);

            //una consulta a la base de datos siempre trataremos de tomar el control, por si pasa algun error y se cierre la aplicación
            try
            {
                //cargamos los tipos de comprobante
                DataSet dset = FTipoDocumento.GetAll();
                dtable = dset.Tables[0];
                cmbTipoDocumento.DataSource = dtable;
                cmbTipoDocumento.ValueMember = "Id";
                cmbTipoDocumento.DisplayMember = "Nombre";

                //mostrando el detalle d elas ventas o los items si es que hay un id cargado
                if (lblCompraId.Text != "000-000")
                {
                    DataSet dsItems = FDetalleCompra.GetAll(Convert.ToInt32(lblCompraId.Text));
                    dtItems = dsItems.Tables[0];
                    dgvItems.DataSource = dtItems;

                    dgvItems.Columns["Cantidad"].Width = 45;
                    dgvItems.Columns["Codigo"].Width = 58;
                    dgvItems.Columns["Nombre"].Width = 240;
                    dgvItems.Columns["PrecioCosto"].Width = 55;
                    dgvItems.Columns["PrecioCosto"].HeaderText = "Precio U";
                    dgvItems.Columns["Importe"].Width = 60;

                    dgvItems.Columns["Nombre"].Visible = true;
                    dgvItems.Columns["Codigo"].Visible = true;
                    dgvItems.Columns["Id"].Visible = false;
                    dgvItems.Columns["CompraId"].Visible = false;
                    dgvItems.Columns["ProductoId"].Visible = false;
                    dgvItems.Columns["PrecioCosto"].Visible = true;
                    dgvItems.Columns["Importe"].Visible = true;
                    dgvItems.Columns["TipoDocumento"].Visible = false;

                    dgvItems.Columns["Unidad"].Visible = true;
                    


                    cmbTipoDocumento.Text = dtItems.Rows[0]["TipoDocumento"].ToString();


                    if (dtItems.Rows.Count > 0)
                    {
                        lblSinDatos.Visible = false;
                        calcularMontoComprobante();

                    }//fin de agregar items a la venta creada



                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        //2c insertar producto--- el 3 se encuentra en FrmProveedores
        public void InsertarProducto(string id, string nombre, string direccion, string ruc)
        {
            txtIdProveedor.Text = id;
            txtNombreProveedor.Text = nombre;
            txtDireccionProveedor.Text = direccion;
            txtRucProveedor.Text = ruc;
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            //5c para pasar 1 al txtInsertarProveedor de FrmProveedores... el ultimo paso 6c esta en FrmProveedores
            FrmProveedores formularioProv = new FrmProveedores();
            formularioProv.InsertarProveedorACompras("1");
            formularioProv.ShowDialog();
        }
        double precioVenta;
        double impuesto;
        double valorVenta;
        public void calcularMontoComprobante()
        {
            try
            {

                if (cmbTipoDocumento.Text == "Seleccione")
                {
                    MessageBox.Show("Seleccione el tipo de comprobante a emitir", "Seleccione un tipo de comprobante de venta");
                }

                if (cmbTipoDocumento.Text == "Boleta")
                {
                    MessageBox.Show("No se puede ingresar una compra hecha con Boleta de venta, tiene que pedir factura...");
                }

                if (cmbTipoDocumento.Text == "Factura")
                {
                    foreach (DataGridViewRow row in dgvItems.Rows)
                    {
                        precioVenta += (Convert.ToDouble(row.Cells["Importe"].Value));
                        //row.Cells["Importe"].Value = Convert.ToDouble(row.Cells["Cant"].Value) * Convert.ToDouble(row.Cells["PrecioUnitario"].Value);
                        valorVenta = precioVenta / 1.18;
                        impuesto = precioVenta - valorVenta;

                        txtIgv.Text = Convert.ToString(impuesto.ToString("0.00", CultureInfo.InvariantCulture));
                        txtPrecioVenta.Text = Convert.ToString(precioVenta.ToString("0.00", CultureInfo.InvariantCulture));
                        txtValorVenta.Text = Convert.ToString(valorVenta.ToString("0.00", CultureInfo.InvariantCulture));

                        txtIgv.Visible = true;
                        lblIgv.Visible = true;

                        lblValorVenta.Visible = true;
                        txtValorVenta.Visible = true;
                    }
                    valorVenta = 0;
                    precioVenta = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }


        }

        private void cmbTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            calcularMontoComprobante();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string sResultado = validarDatos();
                if (sResultado == "") //todo bien
                {
                    if (lblCompraId.Text == "000-000")
                    {
                        //Id, ClienteId, UsuarioId, TipoDocumentoId, Fecha, NumeroDocumento
                        Compra compra = new Compra();
                        compra.Proveedor.Id = Convert.ToInt32(txtIdProveedor.Text);
                        compra.TipoDocumento.Id = Convert.ToInt32(cmbTipoDocumento.SelectedValue.ToString());
                        compra.Fecha = dtpFecha.Value;
                        compra.NumeroDocumento = txtNumeroDocumento.Text;

                        int returnId = FCompra.Insertar(compra, Usuario.Id);
                        if (returnId > 0)
                        {
                            MessageBox.Show("La Compra con identificador único: " + returnId + " fué creada con exito. \n Ahora agregue productos...");
                            lblCompraId.Text = Convert.ToString(returnId);
                            MostrarGuardarCancelar(false);
                            productosImprimir(true);

                        }
                    }
                    else
                    {
                        //Id, ClienteId, UsuarioId, TipoDocumentoId, Fecha, NumeroDocumento
                        Compra compra = new Compra();
                        compra.Id = Convert.ToInt32(lblCompraId.Text);
                        compra.Proveedor.Id = Convert.ToInt32(txtIdProveedor.Text);
                        compra.TipoDocumento.Id = Convert.ToInt32(cmbTipoDocumento.SelectedValue.ToString());
                        compra.Fecha = dtpFecha.Value;
                        compra.NumeroDocumento = txtNumeroDocumento.Text;

                        int returnId = FCompra.Actualizar(compra, Usuario.Id);//cambiar a id usurio
                        if (returnId > 0)
                        {
                            MessageBox.Show("La Compra con identificador único: " + lblCompraId.Text + " se actualizo.");
                            MostrarGuardarCancelar(false);
                            productosImprimir(true);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Error: \n" + sResultado);
                }//fin validacion datos
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private string validarDatos()
        {
            string resultado = "";
            if (cmbTipoDocumento.Text == "Seleccione")
            {
                resultado = "Tiene que seleccionar un tipo de comprobante de compra. \n - En seguida el cursor se ubicará en el lugar indicado.";
                cmbTipoDocumento.Focus();
            }
            if (txtIdProveedor.Text == "")
            {
                resultado = "Tiene que seleccionar un Proveedor. \n - En seguida el cursor se ubicará en el lugar indicado.";
                btnProveedores.Focus();
            }
            if (txtNumeroDocumento.Text == "")
            {
                resultado = "Tiene que escribir el numero de comprobante... \n - En seguida el cursor se ubicará en el lugar indicado.";
                txtNumeroDocumento.Focus();
            }
            return resultado;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
            MostrarGuardarCancelar(true);
            txtNumeroDocumento.Focus();
        }
        public void limpiar()
        {
            lblCompraId.Text = "000-000";
            //cmbTipoDocumento.Text = "Seleccione";
            dgvItems.DataSource = null;
            txtNombreProveedor.Text = "";
            txtDireccionProveedor.Text = "";
            txtIdProveedor.Text = "";
            txtRucProveedor.Text = "";
            txtNumeroDocumento.Text = "";
            txtValorVenta.Text = "";
            txtIgv.Text = "";
            txtPrecioVenta.Text = "";
        }

        public void MostrarGuardarCancelar(bool b)
        {
            btnGuardar.Visible = b;
            btnCancelar.Visible = b;
            btnNuevo.Visible = !b;
            btnEditar.Visible = !b;

            btnProveedores.Enabled = b;

            txtNumeroDocumento.Enabled = b;
            cmbTipoDocumento.Enabled = b;

            dtpFecha.Enabled = b;

        }

        public void productosImprimir(bool c)
        {
            btnProducto.Enabled = c;
            btnImprimir.Enabled = c;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(false);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
        }

        private void dgvItems_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    e.SuppressKeyPress = true;
                    DetalleCompra detalleCompra = new DetalleCompra();
                    detalleCompra.Id = Convert.ToInt32(dgvItems.CurrentRow.Cells["Id"].Value.ToString());
                    
                    FDetalleCompra.Eliminar(detalleCompra);
                    FrmComprasAsistente_Load(null, null);
                    productosImprimir(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dgvItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editaritems();
        }
        private void editaritems()
        {
            FrmModificarItemC edit = new FrmModificarItemC();

            edit.lblDetalleCompraId.Text = dgvItems.CurrentRow.Cells["Id"].Value.ToString();
            edit.txtDescripcionProducto.Text = dgvItems.CurrentRow.Cells["Nombre"].Value.ToString();
            edit.txtCantidad.Text = dgvItems.CurrentRow.Cells["Cantidad"].Value.ToString();
            edit.ventaId = Convert.ToInt32(lblCompraId.Text);
            edit.productoId = Convert.ToInt32(dgvItems.CurrentRow.Cells["ProductoId"].Value.ToString());
            edit.precioCosto = Convert.ToDouble(dgvItems.CurrentRow.Cells["PrecioCosto"].Value.ToString());
            DialogResult res = edit.ShowDialog();

            FrmComprasAsistente_Load(null, null);
            productosImprimir(true);

        }

        private void FrmComprasAsistente_FormClosing(object sender, FormClosingEventArgs e)
        {

                e.Cancel = true;
                this.Hide();
                this.Parent = null;
    

        }

        private void btnProducto_Click(object sender, EventArgs e)
        {
            FrmBuscarProductoC buscarProducto = new FrmBuscarProductoC();
            buscarProducto.compraid = lblCompraId.Text;
            DialogResult res = buscarProducto.ShowDialog();
            FrmComprasAsistente_Load(null, null);
            calcularMontoComprobante();
            productosImprimir(true);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            EditarAppConfig leerConfig = new EditarAppConfig();

            Numalet let;//clase para convertir numeros a letras
            let = new Numalet();
            let.LetraCapital = true;
            let.MascaraSalidaDecimal = "00/100";
            let.Decimales = 2;
            let.ApocoparUnoParteEntera = false;


            if (cmbTipoDocumento.Text == "Factura")
            {

                Report frmComprobantes = new Report(); // clase adicional para crear reportes... de aquí solo pasamos la cadena de coneccion, en el mismo reporteador está el script sql, por lo tanto para modificar las apariencias se debera instalar Fastreport .net
                frmComprobantes.Load(@"Reportes/FacturaCompra.frx");
                frmComprobantes.Dictionary.Connections[0].ConnectionString = leerConfig.leerValorConfiguracion("connectionString");
                frmComprobantes.SetParameterValue("titulo", "Factura " + leerConfig.leerValorConfiguracion("NombreEmpresa"));
                frmComprobantes.SetParameterValue("CompraId", Convert.ToInt32(lblCompraId.Text));
                frmComprobantes.SetParameterValue("TotalEnLetras", let.ToCustomCardinal(Convert.ToDouble(txtPrecioVenta.Text, CultureInfo.InvariantCulture)) + " Nuevos Soles");
                frmComprobantes.SetParameterValue("SubTotal", txtValorVenta.Text);
                frmComprobantes.SetParameterValue("IGV", txtIgv.Text);
                frmComprobantes.SetParameterValue("Total", txtPrecioVenta.Text);
                frmComprobantes.Show();
            }
        }


    }
}
