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

namespace SistemaVentas.Presentacion.Productos
{
    public partial class FrmEditarProductos : Form
    {
        public FrmEditarProductos()
        {
            InitializeComponent();
        }

        //-------------------------para seleccionar uma categoria o un proveedor y static para que no se pierdan los datos------------------------
        private static FrmEditarProductos _instancia;
        public static FrmEditarProductos InstanciaUnica()
        {
            if (_instancia == null)
                _instancia = new FrmEditarProductos();
            return _instancia;
        }

        public string presentacionSeleccionado;
        public string laboratorioSeleccionado;
        public string seccionSeleccionado;
        
        private void FrmEditarProductos_Load(object sender, EventArgs e)
        {
            if (Usuario.Rol == "Vendedor")
            {
                mostrarOpcionesUsuario(false);
            }
            try
            {
                DataTable dtable                = new DataTable();
                DataSet dset                    = FPresentacion_.GetAll();
                dtable                          = dset.Tables[0];
                cmbPresentacion.DataSource      = dtable;
                cmbPresentacion.ValueMember     = "Id";
                cmbPresentacion.DisplayMember   = "Descripcion";
                cmbPresentacion.Text            = presentacionSeleccionado;

                DataTable dtLab = new DataTable();
                DataSet dsetLab = FLaboratorio.GetAll();
                dtLab = dsetLab.Tables[0];
                cmbLaboratorio.DataSource = dtLab;
                cmbLaboratorio.ValueMember = "Id";
                cmbLaboratorio.DisplayMember = "Descripcion";
                //cmbLaboratorio.SelectedIndex = 2;
                cmbLaboratorio.Text = laboratorioSeleccionado;


                DataTable dtSecc         = new DataTable();
                DataSet dsetSecc         = FSeccion.GetAll();
                dtSecc                   = dsetSecc.Tables[0];
                cmbSeccion.DataSource    = dtSecc;
                cmbSeccion.ValueMember   = "Id";
                cmbSeccion.DisplayMember = "Descripcion";
                cmbSeccion.Text          = seccionSeleccionado;

                cargarLotes();
                cargarCategorias();
                cargarComposicion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }
        //-------------------------lotee----------------------------------------
        private void guargarLote()
        {
            try
            {
                if (txtAgregarLote.Text != "")
                {
                    Lote lote = new Lote();
                    lote.Descripcion = txtAgregarLote.Text;

                    int returnIdLote = FLote.Insertar(lote);
                    if (returnIdLote > 0)
                    {
                        ProductoLote plote = new ProductoLote();
                        plote.Producto.Id = Convert.ToInt32(txtId.Text);
                        plote.Lote.Id = returnIdLote;
                        FProductoLote.Insertar(plote);

                        cargarLotes();
                    }
                }
                else
                    MessageBox.Show("Escriba el numero del lote", "Escriba el Número del lote");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private static DataTable dtLote = new DataTable();
        private void cargarLotes()
        {

            try
            {
                DataSet ds = FProductoLote.Get(Convert.ToInt32(txtId.Text));
                dtLote = ds.Tables[0];
                dgvLote.DataSource = dtLote;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        //--------------------------------------------categoria-----------------------------------------
        private void guargarCategoria()
        {
            try
            {
                if (txtAgregarCategoria.Text != "")
                {
                    Categoria categoria = new Categoria();
                    categoria.Descripcion = txtAgregarCategoria.Text;

                    int returnIdCategoria = FCategoria.Insertar(categoria);
                    if (returnIdCategoria > 0)
                    {
                        ProductoCategoria pCate = new ProductoCategoria();
                        pCate.Producto.Id = Convert.ToInt32(txtId.Text);
                        pCate.Categoria.Id = returnIdCategoria;
                        FProductoCategoria.Insertar(pCate);

                        cargarCategorias();
                    }
                }
                else
                    MessageBox.Show("Escriba el numero del lote", "Escriba el Número del lote");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private static DataTable dtCategoria = new DataTable();
        private void cargarCategorias()
        {
            try
            {
                DataSet ds = FProductoCategoria.Get(Convert.ToInt32(txtId.Text));
                dtCategoria = ds.Tables[0];
                dgvCategoria.DataSource = dtCategoria;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }


        //-----------------------------------composicion-------------------------------------------------
        private void guargarComposicion()
        {
            try
            {
                if (txtAgregarComposicion.Text != "")
                {
                    Composicion composicion = new Composicion();
                    composicion.Descripcion = txtAgregarComposicion.Text;

                    int returnIdComposicion = FComposicion.Insertar(composicion);
                    if (returnIdComposicion > 0)
                    {
                        ProductoComposicion pCompo = new ProductoComposicion();
                        pCompo.Producto.Id = Convert.ToInt32(txtId.Text);
                        pCompo.Composicion.Id = returnIdComposicion;
                        FProductoComposicion.Insertar(pCompo);

                        cargarComposicion();
                    }
                }
                else
                    MessageBox.Show("Escriba el numero del lote", "Escriba el Número del lote");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private static DataTable dtComposicion = new DataTable();
        private void cargarComposicion()
        {

            try
            {
                DataSet ds = FProductoComposicion.Get(Convert.ToInt32(txtId.Text));
                dtComposicion = ds.Tables[0];
                dgvComposicion.DataSource = dtComposicion;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }


        private void mostrarOpcionesUsuario(bool b)
        {
            txtStock.Enabled = b;
            txtPrecioCompra.Enabled = b;
            txtPrecioVenta.Enabled = b;

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
                        producto.Id = Convert.ToInt32(txtId.Text);
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

                        if (FProducto.Actualizar(producto) > 0)
                        {
                            MessageBox.Show(txtNombre.Text + " se modificó correctamente");
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


        private void btnCategoria_Click(object sender, EventArgs e)
        {
            FrmCategorias formulariocategoria = new FrmCategorias();
            formulariocategoria.EditarCat("1");
            formulariocategoria.ShowDialog();
        }

        private void btnProveedor_Click(object sender, EventArgs e)
        {
            FrmProveedores form = new FrmProveedores();
            form.EditarProveedor("1");
            form.ShowDialog();
        }

        //para recibir datos de proveedor
        public void InsertarProveedor(string id, string nombre)
        {
            txtIdProveedor.Text = id;
            txtProveedor.Text = nombre;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Parent = null;
        }

        private void btnGuardarLote_Click(object sender, EventArgs e)
        {
            guargarLote();
        }

        private void txtAgregarLote_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    guargarLote();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnGuargarComposicion_Click(object sender, EventArgs e)
        {
            guargarComposicion();
        }

        private void btnGuardarCategoria_Click(object sender, EventArgs e)
        {
            guargarCategoria();
        }

        private void txtAgregarComposicion_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    guargarComposicion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void txtAgregarCategoria_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    guargarCategoria();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void FrmEditarProductos_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }


    }
}
