using FastReport;
using SistemaVentas.Datos;
using SistemaVentas.Entidad;
using SistemaVentas.Presentacion.Clientes;
using SisttVentas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SistemaVentas.Presentacion.VentasAsistente
{
    public partial class FrmVentasAsistente : Form
    {
        public FrmVentasAsistente()
        {
            InitializeComponent();
        }

        //1p  instancia unica
        private static FrmVentasAsistente _instancia;
        public static FrmVentasAsistente InstanciaUnica()
        {
            if (_instancia == null)
                _instancia = new FrmVentasAsistente();
            return _instancia;
        }

        private static DataTable dt = new DataTable(); //variable para crear un tabla
        private static DataTable dtItems = new DataTable();//variable para crear un tabla para items de vent
        private static DataTable dtable = new DataTable();//variable para crear un tabla para los tipos de comprobante
        private void FrmVentas_Load(object sender, EventArgs e)
        {
           
            MostrarGuardarCancelar(false);
            productosImprimir(false);
            
            //una consulta a la base de datos siempre trataremos de omar el control, por si pasa algun error y se cierre la aplicación
            try
            {
                //cargamos los tipos de comprobante
                DataSet dset = FTipoDocumento.GetAll();
                dtable = dset.Tables[0];
                cmbTipoDocumento.DataSource = dtable;
                cmbTipoDocumento.ValueMember = "Id";
                cmbTipoDocumento.DisplayMember = "Nombre";

               //mostrando el detalle d elas ventas o los items si es que hay un id cargado
                if (lblVentaId.Text != "000-000")
                {

                    DataSet dsItems = FDetalleVenta.GetAll(Convert.ToInt32(lblVentaId.Text));
                    dtItems = dsItems.Tables[0];
                    dgvItems.DataSource = dtItems;

                    dgvItems.Columns["Cantidad"].Width = 40;
                    dgvItems.Columns["Cantidad"].HeaderText = "Cant.";
                    dgvItems.Columns["Codigo"].Width = 60;
                    dgvItems.Columns["Nombre"].Width = 250;
                    dgvItems.Columns["PrecioUnitario"].Width = 70;
                    dgvItems.Columns["PrecioUnitario"].HeaderText = "Precio U";
                    dgvItems.Columns["Importe"].Width = 100;

                    dgvItems.Columns["Nombre"].Visible = true;
                    dgvItems.Columns["Codigo"].Visible = true;
                    dgvItems.Columns["Id"].Visible = false;
                    dgvItems.Columns["VentaId"].Visible = false;
                    dgvItems.Columns["ProductoId"].Visible = false;
                    dgvItems.Columns["Importe"].Visible = true;
                    dgvItems.Columns["TipoDocumento"].Visible = false;
                    


                    cmbTipoDocumento.Text = dtItems.Rows[0]["TipoDocumento"].ToString();

                    if (dtItems.Rows.Count > 0)
                    {
                        lblSinDatos.Visible = false;
                        calcularMontoComprobante();
                        productosImprimir(true);

                    }//fin de agregar items a la venta creada
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.StackTrace);
            }


        }

        //2p insertar producto--- el 3 se encuentra en FrmClientes
        public void InsertarProducto(string id, string nombre,string  direccion,string ruc)
        {
            txtIdCliente.Text = id;
            txtNombreCliente.Text = nombre;
            txtDireccionCliente.Text = direccion;
            txtRucCliente.Text = ruc;
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            //5p para pasar 1 al txtInsertarCliente de FrmClientes... el ultimo paso está en FrmClientes
            FrmClientes formulariocaCliente = new FrmClientes();
            formulariocaCliente.InsertarCliente("1");
            formulariocaCliente.ShowDialog();
        }


        double precioVenta;
        double impuesto;
        double valorVenta;
        public void calcularMontoComprobante()
        {


            if (cmbTipoDocumento.Text == "Seleccione")
            {
                MessageBox.Show("Seleccione el tipo de comprobante a emitir","Seleccione un tipo de comprobante de venta");
            }

            if (cmbTipoDocumento.Text == "Boleta")
            {
                foreach (DataGridViewRow row in dgvItems.Rows)
                {
                    precioVenta += (Convert.ToDouble(row.Cells["Importe"].Value));
                    //row.Cells["Importe"].Value=Convert.ToDouble(row.Cells["Cant"].Value)*Convert.ToDouble(row.Cells["PrecioUnitario"].Value);
                    txtPrecioVenta.Text = Convert.ToString(precioVenta.ToString("0.00",CultureInfo.InvariantCulture));
                }

                precioVenta = 0;
                valorVenta = 0;
                lblIgv.Visible = false;
                txtIgv.Visible = false;
                lblValorVenta.Visible = false;
                txtValorVenta.Visible = false;
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

        private void cmbTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            calcularMontoComprobante();
            //MessageBox.Show(cmbTipoDocumento.SelectedValue.ToString());
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string sResultado = validarDatos();
                if (sResultado == "") //todo bien
                {
                    if(lblVentaId.Text=="000-000")
                    {
                        //Id, ClienteId, UsuarioId, TipoDocumentoId, Fecha, NumeroDocumento
                       Venta venta = new Venta();
                       venta.Cliente.Id = Convert.ToInt32(txtIdCliente.Text);
                       venta.TipoDocumento.Id = Convert.ToInt32(cmbTipoDocumento.SelectedValue.ToString());
                       venta.Fecha = dtpFecha.Value;
                       venta.NumeroDocumento = txtNumeroDocumento.Text;

                       int returnId = FVenta.Insertar(venta,Usuario.Id);
                       if (returnId > 0)
                       {
                         MessageBox.Show("La venta con identificador único " +returnId+" fué creada con exito. \n Ahora agregue productos...");
                         lblVentaId.Text = Convert.ToString(returnId);
                         txtNumeroDocumento.Text = Convert.ToString(returnId);
                         MostrarGuardarCancelar(false);
                         productosImprimir(true);

                         abrirBuscador();
                         
                       }
                    }
                    else
                    {
                        //Id, ClienteId, UsuarioId, TipoDocumentoId, Fecha, NumeroDocumento
                        Venta venta = new Venta();
                        venta.Id = Convert.ToInt32(lblVentaId.Text);
                        venta.Cliente.Id = Convert.ToInt32(txtIdCliente.Text);
                        venta.TipoDocumento.Id = Convert.ToInt32(cmbTipoDocumento.SelectedValue.ToString());
                        venta.Fecha = dtpFecha.Value;
                        venta.NumeroDocumento = txtNumeroDocumento.Text;

                        int returnId = FVenta.Actualizar(venta, Usuario.Id);//cambiar a id usurio
                        if (returnId > 0)
                        {
                            MessageBox.Show("La venta con identificador único " + lblVentaId.Text + " se actualizo.");
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
                resultado = "Tiene que seleccionar un tipo de comprobante de venta. \n - En seguida el cursor se ubicará en el lugar indicado.";
                cmbTipoDocumento.Focus();
            }
            if (txtIdCliente.Text == "")
            {
                resultado = "Tiene que seleccionar un cliente. \n - En seguida el cursor se ubicará en el lugar indicado.";
                btnClientes.Focus();
            }
            //if (txtNumeroDocumento.Text == "")
            //{
            //    resultado = "Tiene que escribir el numero de comprobante... \n - En seguida el cursor se ubicará en el lugar indicado.";
            //    txtNumeroDocumento.Focus();
            //}
            return resultado;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            limpiar();
            MostrarGuardarCancelar(true);

        }

        public void limpiar()
        {
            lblVentaId.Text = "000-000";
            //cmbTipoDocumento.Text = "Seleccione";
            dgvItems.DataSource = null;
            txtNombreCliente.Text = "Cliente de Mostrador";
            txtDireccionCliente.Text = "";
            txtIdCliente.Text = "1";
            txtRucCliente.Text = "";
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

            btnClientes.Enabled = b;
            
            cmbTipoDocumento.Enabled = b;
            
            dtpFecha.Enabled = b;

        }

        public void productosImprimir(bool c)
        {
            btnProducto.Enabled = c; 
            btnImprimir.Enabled = c;
        }
        private void btnProducto_Click(object sender, EventArgs e)
        {

            abrirBuscador();
        }

        private void abrirBuscador()
        {
            FrmBuscarProducto buscarProducto = new FrmBuscarProducto();
            buscarProducto.ventaid = lblVentaId.Text;
            DialogResult res = buscarProducto.ShowDialog();
            FrmVentas_Load(null, null);
            calcularMontoComprobante();
            productosImprimir(true);
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(false);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
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
                frmComprobantes.Load(@"Reportes/FacturaVenta.frx");
                frmComprobantes.Dictionary.Connections[0].ConnectionString = leerConfig.leerValorConfiguracion("connectionString");
                frmComprobantes.SetParameterValue("titulo", "Factura " + leerConfig.leerValorConfiguracion("NombreEmpresa"));
                frmComprobantes.SetParameterValue("VentaId", Convert.ToInt32(lblVentaId.Text));
                frmComprobantes.SetParameterValue("TotalEnLetras", let.ToCustomCardinal(Convert.ToDouble(txtPrecioVenta.Text,CultureInfo.InvariantCulture)) + " Nuevos Soles");
                frmComprobantes.SetParameterValue("SubTotal", txtValorVenta.Text);
                frmComprobantes.SetParameterValue("IGV", txtIgv.Text);
                frmComprobantes.SetParameterValue("Total", txtPrecioVenta.Text);
                frmComprobantes.Show();
            }
            else if (cmbTipoDocumento.Text == "Boleta")
            {

                Report frmComprobantes = new Report(); // clase adicional para crear reportes... de aquí solo pasamos la cadena de coneccion, en el mismo reporteador está el script sql, por lo tanto para modificar las apariencias se debera instalar Fastreport .net
                frmComprobantes.Load(@"Reportes/BoletaVenta.frx");
                frmComprobantes.Dictionary.Connections[0].ConnectionString = leerConfig.leerValorConfiguracion("connectionString");
                frmComprobantes.SetParameterValue("titulo", "Boleta " + leerConfig.leerValorConfiguracion("NombreEmpresa"));
                frmComprobantes.SetParameterValue("VentaId", Convert.ToInt32(lblVentaId.Text));
                frmComprobantes.SetParameterValue("TotalEnLetras", let.ToCustomCardinal(Convert.ToDouble(txtPrecioVenta.Text, CultureInfo.InvariantCulture)) + " Nuevos Soles");
                frmComprobantes.SetParameterValue("Total", txtPrecioVenta.Text);
                frmComprobantes.Show();
            }
        }

        private void dgvItems_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    e.SuppressKeyPress = true;

                    DetalleVenta detalleVenta = new DetalleVenta();
                    detalleVenta.Id =  Convert.ToInt32(dgvItems.CurrentRow.Cells["Id"].Value.ToString());
                    
                    FDetalleVenta.Eliminar(detalleVenta);
                    FrmVentas_Load(null, null);
                    productosImprimir(true);
                }
                //else if (e.KeyCode == Keys.Enter)
                //{
                //    e.SuppressKeyPress = true;

                //    DetalleVenta detalleVenta = new DetalleVenta();
                //    detalleVenta.Id = Convert.ToInt32(dgvItems.CurrentRow.Cells["Id"].Value.ToString());
                //    MessageBox.Show(detalleVenta.Id.ToString());
                //    FDetalleVenta.Eliminar(detalleVenta);
                //    FrmVentas_Load(null, null);
                //}  
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
            FrmModificarItem edit = new FrmModificarItem();

            edit.lblDetalleVentaId.Text = dgvItems.CurrentRow.Cells["Id"].Value.ToString();
            edit.txtDescripcionProducto.Text = dgvItems.CurrentRow.Cells["Nombre"].Value.ToString();
            edit.txtCantidad.Text = dgvItems.CurrentRow.Cells["Cantidad"].Value.ToString();
            edit.ventaId = Convert.ToInt32(lblVentaId.Text);
            edit.productoId=Convert.ToInt32(dgvItems.CurrentRow.Cells["ProductoId"].Value.ToString());
            edit.precioUnitario = Convert.ToDouble(dgvItems.CurrentRow.Cells["PrecioUnitario"].Value.ToString());
            DialogResult res = edit.ShowDialog();

            FrmVentas_Load(null,null);

            productosImprimir(true);
        }

        private void FrmVentasAsistente_FormClosing(object sender, FormClosingEventArgs e)
        {

                this.Hide();
                this.Parent = null;
                e.Cancel = true;
          
        }

    }
}
