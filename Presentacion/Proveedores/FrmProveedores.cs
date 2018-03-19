using SistemaVentas.Datos;
using SistemaVentas.Entidad;
using SistemaVentas.Presentacion.Compras;
using SistemaVentas.Presentacion.Productos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SistemaVentas.Presentacion.Proveedores
{
    public partial class FrmProveedores : Form
    {
        public FrmProveedores()
        {
            InitializeComponent();
        }
        private static DataTable dt = new DataTable();
        private void FrmProveedores_Load(object sender, EventArgs e)
        {
            
            //una consulta a la base de datos siempre trataremos de omar el control, por si pasa algun error y se cierre la aplicación
            try
            {
                DataSet ds = FProveedor.GetAll();//creamos un dataset, un contenedor de datos y hacemos una consulta con la clase FCategoria y su método GetAll
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAgregarProveedor addDato = new FrmAgregarProveedor();
            DialogResult res = addDato.ShowDialog();

            FrmProveedores_Load(null, null);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Está seguro de eliminar el dato seleccionados?", "Eliminando...",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    Proveedor proveedor = new Proveedor();// instanciamo nuestra clase para rellenarle sus atributos
                    proveedor.Id = Convert.ToInt32(dgvDatos.CurrentRow.Cells["Id"].Value.ToString());// para eliminar solo le pasaremos su atributo id

                    if (FProveedor.Eliminar(proveedor) > 0)// Ejecutamos el procedimiento almacenado para eliminar, si se pudo eliminar... recargamos el formulario
                    {
                        FrmProveedores_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar, es más probable que tenga productos asociadas a este proveedor \nPrimero deberá eliminar todas las productos relacionados a este proveedor", "No se puede eliminar");

                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.StackTrace);
                MessageBox.Show("No se pudo eliminar, es más probable que tenga productos asociadas a este proveedor \nPrimero deberá eliminar todas las productos relacionados a este proveedor", "No se puede eliminar");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            FrmProveedores_Load(null, null);
        }

        int fila;
        int columna;
        private void btnEditar_Click(object sender, EventArgs e)
        {
            FrmEditarProveedores frmMod = new FrmEditarProveedores();

            if (dgvDatos.CurrentRow != null)
            {
                //si esta seleccionado algun dato obtenemos el id del dato para pasar al otro formulrion, para su respectiva edicion, en el otro form el texbox, su popiedad  modifier esta en publico, no es la manera mas correcta de hacerlo pero el mas facil
                frmMod.txtId.Text = dgvDatos.CurrentRow.Cells["Id"].Value.ToString();
                frmMod.txtRuc.Text = dgvDatos.CurrentRow.Cells["Ruc"].Value.ToString();             
                frmMod.txtNombre.Text = dgvDatos.CurrentRow.Cells["Nombre"].Value.ToString();               
                frmMod.txtEmail.Text = dgvDatos.CurrentRow.Cells["Email"].Value.ToString();
                frmMod.txtTelefono.Text = dgvDatos.CurrentRow.Cells["Telefono"].Value.ToString();
                frmMod.txtDireccion.Text = dgvDatos.CurrentRow.Cells["Direccion"].Value.ToString();
                frmMod.dtpFechaRegistro.Text = dgvDatos.CurrentRow.Cells["FechaRegistro"].Value.ToString();

                fila = dgvDatos.CurrentCellAddress.X; // obtenemos el indice de la fila seleccionada
                columna = dgvDatos.CurrentCellAddress.Y;// obtenemos el indice de la columna seleccionada
            }

            DialogResult res = frmMod.ShowDialog(); //mostramos el formulario nuevo como dialogo modal, para cuando se cierre hacer una accion en el form princiapal

            FrmProveedores_Load(null, null);// se cierra el anterior formulario de editar y recargamos el formularioCategoria para actualizar datos modificados
            dgvDatos.CurrentCell = dgvDatos.Rows[columna].Cells[fila];
        }
        //3c esto es para seleccionar un producto desde compra
        internal void InsertarProveedorACompras(string valor)
        {
            txtInsertarProveedor.Text = valor;
        }
        //4c Esto es para pasar datos a las Compras... el 5c se encuentra en FrmComprasAsistente
        private void pasarDatosACompras()
        {
            if (txtInsertarProveedor.Text == "1")
            {
                FrmComprasAsistente formulario = FrmComprasAsistente.InstanciaUnica();
                if (dgvDatos.CurrentRow != null)
                {
                    formulario.InsertarProducto(dgvDatos.CurrentRow.Cells["Id"].Value.ToString(), dgvDatos.CurrentRow.Cells["Nombre"].Value.ToString(), dgvDatos.CurrentRow.Cells["Direccion"].Value.ToString(), dgvDatos.CurrentRow.Cells["Ruc"].Value.ToString());
                    formulario.Show();
                    Close();
                }
            }
        }
        //4 ============================todo para pasar datos a editar productos=====================================//
        private void pasarDatosAProductoInsertar()
        {
            if (txtInsertar.Text == "1")
            {
                FrmAgregarProductos formularioProducto = FrmAgregarProductos.InstanciaUnica();
                if (dgvDatos.CurrentRow != null)
                {                   
                    formularioProducto.InsertarProveedor(dgvDatos.CurrentRow.Cells["Id"].Value.ToString(), dgvDatos.CurrentRow.Cells["Nombre"].Value.ToString());
                    formularioProducto.Show();
                    Close();
                }
            }
        }

        private void pasarDatosAProductoEDitar()
        {
            if (txtEditar.Text == "1")
            {
                FrmEditarProductos formularioProducto = FrmEditarProductos.InstanciaUnica();
                if (dgvDatos.CurrentRow != null)
                {
                    formularioProducto.InsertarProveedor(dgvDatos.CurrentRow.Cells["Id"].Value.ToString(), dgvDatos.CurrentRow.Cells["Nombre"].Value.ToString());
                    formularioProducto.Show();
                    Close();
                }
            }
        }

        internal void InsertarProveedor(string valor)
        {
            txtInsertar.Text = valor;
        }

        internal void EditarProveedor(string valor)
        {
            txtEditar.Text = valor;
        }
        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(txtEditar.Text=="1")
            {
                pasarDatosAProductoEDitar();
            }
            else if (txtInsertar.Text == "1")
            {
                pasarDatosAProductoInsertar();
            }
            else if(txtInsertarProveedor.Text=="1")
            {
              //6c pasar datos en si al form FrmVentas  y fin de pasar proveedor a compras
                pasarDatosACompras();
            }
        }

        private void dgvDatos_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;

                    pasarDatosACompras();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
