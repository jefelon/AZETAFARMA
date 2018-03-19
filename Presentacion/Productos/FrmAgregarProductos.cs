using SistemaVentas.Datos;
using SistemaVentas.Entidad;
using SistemaVentas.Presentacion.Categorias;
using SistemaVentas.Presentacion.Proveedores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SistemaVentas.Presentacion.Lotes;

namespace SistemaVentas.Presentacion.Productos
{
    public partial class FrmAgregarProductos : Form
    {
        public FrmAgregarProductos()
        {
            InitializeComponent();
        }
        //2) para seleccionar uma categoria o un proveedor y static para que no se pierdan los datos------------------------
        private static FrmAgregarProductos _instancia;
        public static FrmAgregarProductos InstanciaUnica()
        {
            if (_instancia == null)
                _instancia = new FrmAgregarProductos();
            return _instancia;
        }

        private void FrmAgregarProductos_Load(object sender, EventArgs e)
        {
            if (Usuario.Rol == "Vendedor")
            {
                mostrarOpcionesUsuario(false);
            }
            if (Usuario.Rol == "Administrador")
            {
                mostrarOpcionesUsuario(true);
            }
            else
            {
                mostrarOpcionesUsuario(false);
            }

            try
            {
                DataTable dtable = new DataTable();
                DataSet dset = FPresentacion_.GetAll();
                dtable = dset.Tables[0];
                cmbPresentacion.DataSource = dtable;
                cmbPresentacion.ValueMember = "Id";
                cmbPresentacion.DisplayMember = "Descripcion";

                DataTable dtLab = new DataTable();
                DataSet dsetLab = FLaboratorio.GetAll();
                dtLab = dsetLab.Tables[0];
                cmbLaboratorio.DataSource = dtLab;
                cmbLaboratorio.ValueMember = "Id";
                cmbLaboratorio.DisplayMember = "Descripcion";

                DataTable dtSecc = new DataTable();
                DataSet dsetSecc = FSeccion.GetAll();
                dtSecc = dsetSecc.Tables[0];
                cmbSeccion.DataSource = dtSecc;
                cmbSeccion.ValueMember = "Id";
                cmbSeccion.DisplayMember = "Descripcion";               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string sResultado = validarDatos();
                if (sResultado == "") //todo bien
                {
                    if (txtNombre.Text != "")
                    {
                        Producto producto = new Producto();
                        producto.Proveedor.Id = Convert.ToInt32(txtIdProveedor.Text);
                        producto.Codigo = txtCodigo.Text;
                        producto.Nombre = txtNombre.Text;
                        producto.PrecioCompra = Convert.ToDouble(txtPrecioCompra.Text);
                        producto.PrecioVenta = Convert.ToDouble(txtPrecioVenta.Text);
                        producto.Stock = Convert.ToDouble(txtStock.Text);
                        producto.StockMinimo = Convert.ToDouble(txtStockMinimo.Text);
                        producto.StockMaximo = Convert.ToDouble(txtStockMaximo.Text);
                        producto.Presentacion_.Id = Convert.ToInt32(cmbPresentacion.SelectedValue);
                        producto.Laboratorio.Id = Convert.ToInt32(cmbLaboratorio.SelectedValue);
                        producto.Ubicacion = txtUbicacion.Text;
                        producto.Vencimiento = dtpVencimiento.Value;
                        producto.Seccion.Id = Convert.ToInt32(cmbSeccion.SelectedValue);
                        producto.Indicacion = txtIndicacion.Text;
                        producto.Composicion = txtComposicion.Text;
                        producto.FechaRegistro = dtpFechaRegistro.Value;
                        //producto.Imagen = ;

                        int returnId =  FProducto.Insertar(producto);
                        if (returnId > 0)
                        {
                            MessageBox.Show(txtNombre.Text + " se agregó correctamente");

                            if (MessageBox.Show("Se han habilitado otras opciones de productos, \n Presione aceptar y le llevaremos a la opciones habilitadas", "Tiene más opciones ver y editar?...",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                //superTabControlProducto.SelectedTab = tabItemLote;

                                FrmEditarProductos frmMod = new FrmEditarProductos();
                                frmMod.txtId.Text = Convert.ToString(returnId);
                                frmMod.txtIdProveedor.Text = txtIdProveedor.Text;
                                frmMod.txtCodigo.Text = txtCodigo.Text;
                                frmMod.txtNombre.Text = txtNombre.Text;
                                frmMod.txtPrecioVenta.Text = txtPrecioVenta.Text;
                                frmMod.txtPrecioCompra.Text = txtPrecioCompra.Text;
                                frmMod.txtStock.Text = txtStock.Text;
                                frmMod.txtStockMinimo.Text = txtStockMinimo.Text;
                                frmMod.txtStockMaximo.Text = txtStockMaximo.Text;
                                frmMod.presentacionSeleccionado = cmbPresentacion.Text;
                                frmMod.laboratorioSeleccionado = cmbLaboratorio.Text;
                                frmMod.txtUbicacion.Text = txtUbicacion.Text;
                                frmMod.dtpVencimiento.Text = dtpVencimiento.Value.ToString();
                                frmMod.seccionSeleccionado = cmbSeccion.Text;
                                frmMod.txtIndicacion.Text = txtIndicacion.Text;
                                frmMod.txtComposicion.Text = txtComposicion.Text;
                                frmMod.dtpFechaRegistro.Text = dtpFechaRegistro.Value.ToString();

                                this.Close();
                                frmMod.Show();
                                frmMod.TopMost = true;
                            }
                            else
                            Close();
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
        public string validarDatos()
        {
            string resultado = "";
            if (txtNombre.Text=="")
            {
                resultado = "El nombre del producto no puede estar vacío.";
                txtNombre.Focus();
            }

            if (txtIdProveedor.Text == "")
            {
                resultado = "Seleccione un proveedor.";
                btnProveedor.Focus();
            }
            return resultado;
        }

        //3)=========== para pasar datos de otros form================
        private void btnCategoria_Click(object sender, EventArgs e)
        {
            FrmCategorias formulariocategoria = new FrmCategorias();
            formulariocategoria.InserttarCat("1");
            formulariocategoria.ShowDialog();
        }

        private void btnProveedor_Click(object sender, EventArgs e)
        {
            FrmProveedores form = new FrmProveedores();
            form.InsertarProveedor("1");
            form.ShowDialog();
        }

        //para recibir datos de proveedor

        public void InsertarProveedor(string id, string nombre)
        {
            txtIdProveedor.Text = id;
            txtProveedor.Text = nombre;
        }
        //=============fin para pasar datos de otro form===================
        private void mostrarOpcionesUsuario(bool b)
        {
            txtStock.Enabled = b;
            txtStock.Text = "0";
            txtPrecioCompra.Enabled = b;
            txtPrecioVenta.Enabled = b;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
       
    }
}
