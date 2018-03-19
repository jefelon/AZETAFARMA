using SistemaVentas.Datos;
using SistemaVentas.Entidad;
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
    public partial class FrmProductos : Form
    {
        public FrmProductos()
        {
            InitializeComponent();
        }

        ////-------------------------para seleccionar uma categoria o un proveedor y static para que no se pierdan los datos------------------------
        //private static FrmProductos _instanciaUnica;
        //public static FrmProductos InstanciaUnica()
        //{
        //    if (_instanciaUnica == null)
        //        _instanciaUnica = new FrmProductos();
        //    return _instanciaUnica;
        //}

        private static DataTable dt = new DataTable();
        private void FrmProductos_Load(object sender, EventArgs e)
        {
            //una consulta a la base de datos siempre trataremos de omar el control, por si pasa algun error y se cierre la aplicación
            try
            {
                DataSet ds = FProducto.GetAll();//creamos un dataset, un contenedor de datos y hacemos una consulta con la clase FCategoria y su método GetAll
                dt = ds.Tables[0]; //asignamos los datos del datased a la tabla
                dgvDatos.DataSource = dt; //propiamente dicho al datagridview le pasamos los datos de la tabla

                if (dt.Rows.Count > 0)//si hay datos, ocultar este label
                {
                    lblSinDatos.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            FrmProductos_Load(null, null);
        }

        //1) para instanciar el form se hace con su funcion InstanciaUnica, para no perder datos
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAgregarProductos addDato =  FrmAgregarProductos.InstanciaUnica();
            DialogResult res = addDato.ShowDialog();

            FrmProductos_Load(null, null);
        }

        int fila;
        int columna;
        private void btnEditar_Click(object sender, EventArgs e)
        {
            FrmEditarProductos frmMod = FrmEditarProductos.InstanciaUnica();//esta instancia ya no se hace new... porque en la funcion InstanciaUnica Ya crea...

            if (dgvDatos.CurrentRow != null)
            {
                //si esta seleccionado algun dato obtenemos el id del dato para pasar al otro formulrion, para su respectiva edicion, en el otro form el texbox, su popiedad  modifier esta en publico, no es la manera mas correcta de hacerlo pero el mas facil
                //   PrecioVenta, PrecioCompra, Stock, Imagen
                frmMod.txtId.Text = dgvDatos.CurrentRow.Cells["Id"].Value.ToString();
                frmMod.txtIdProveedor.Text = dgvDatos.CurrentRow.Cells["ProveedorId"].Value.ToString();
                frmMod.txtCodigo.Text=dgvDatos.CurrentRow.Cells["Codigo"].Value.ToString();
                frmMod.txtNombre.Text = dgvDatos.CurrentRow.Cells["Nombre"].Value.ToString();
                frmMod.txtPrecioVenta.Text = dgvDatos.CurrentRow.Cells["PrecioVenta"].Value.ToString();
                frmMod.txtPrecioCompra.Text = dgvDatos.CurrentRow.Cells["PrecioCompra"].Value.ToString();
                frmMod.txtStock.Text = dgvDatos.CurrentRow.Cells["Stock"].Value.ToString();
                frmMod.txtStockMinimo.Text = dgvDatos.CurrentRow.Cells["StockMinimo"].Value.ToString();
                frmMod.txtStockMaximo.Text = dgvDatos.CurrentRow.Cells["StockMaximo"].Value.ToString();
                frmMod.presentacionSeleccionado = dgvDatos.CurrentRow.Cells["Presentacion"].Value.ToString();
                frmMod.laboratorioSeleccionado = dgvDatos.CurrentRow.Cells["Laboratorio"].Value.ToString();
                frmMod.txtUbicacion.Text = dgvDatos.CurrentRow.Cells["Ubicacion"].Value.ToString();
                frmMod.dtpVencimiento.Text = dgvDatos.CurrentRow.Cells["Vencimiento"].Value.ToString();
                frmMod.seccionSeleccionado = dgvDatos.CurrentRow.Cells["Seccion"].Value.ToString();
                frmMod.txtIndicacion.Text = dgvDatos.CurrentRow.Cells["Indicacion"].Value.ToString();
                frmMod.txtComposicion.Text = dgvDatos.CurrentRow.Cells["Composicion"].Value.ToString();
                frmMod.dtpFechaRegistro.Text = dgvDatos.CurrentRow.Cells["FechaRegistro"].Value.ToString();
                

                fila = dgvDatos.CurrentCellAddress.X; // obtenemos el indice de la fila seleccionada
                columna = dgvDatos.CurrentCellAddress.Y;// obtenemos el indice de la columna seleccionada
            }

            DialogResult res = frmMod.ShowDialog(); //mostramos el formulario nuevo como dialogo modal, para cuando se cierre hacer una accion en el form principal

            FrmProductos_Load(null, null);// se cierra el anterior formulario de editar y recargamos el formularioCategoria para actualizar datos modificados
            dgvDatos.CurrentCell = dgvDatos.Rows[columna].Cells[fila];
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Está seguro de eliminar el dato seleccionados?", "Eliminando...",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    Producto producto = new Producto();// instanciamo nuestra clase para rellenarle sus atributos
                    producto.Id = Convert.ToInt32(dgvDatos.CurrentRow.Cells["Id"].Value.ToString());// para eliminar solo le pasaremos su atributo id

                    if (FProducto.Eliminar(producto) > 0)// Ejecutamos el procedimiento almacenado para eliminar, si se pudo eliminar... recargamos el formulario
                    {
                        FrmProductos_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar, es más probable que tenga ventas asociadas a este producto \nPrimero deberá eliminar todas las ventas con este producto", "No se puede eliminar");

                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.StackTrace);
                MessageBox.Show("No se pudo eliminar, es más probable que tenga ventas asociadas a este producto \nPrimero deberá eliminar todas las ventas con este producto", "No se puede eliminar");
            }
        }



    }
}
