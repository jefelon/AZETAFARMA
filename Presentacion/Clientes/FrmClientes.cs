using SistemaVentas.Datos;
using SistemaVentas.Entidad;
using SistemaVentas.Presentacion.VentasAsistente;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SistemaVentas.Presentacion.Clientes
{
    public partial class FrmClientes : Form
    {
        public FrmClientes()
        {
            InitializeComponent();
        }
        private static DataTable dt = new DataTable();
        private void FrmClientes_Load(object sender, EventArgs e)
        {
            //una consulta a la base de datos siempre trataremos de omar el control, por si pasa algun error y se cierre la aplicación
            try
            {
                DataSet ds = FCliente.GetAll();//creamos un dataset, un contenedor de datos y hacemos una consulta con la clase FCategoria y su método GetAll
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
            FrmAgregarCliente addDato = new FrmAgregarCliente();
            DialogResult res = addDato.ShowDialog();

            FrmClientes_Load(null, null);
        }

        int fila;
        int columna;
        private void btnEditar_Click(object sender, EventArgs e)
        {
            FrmEditarCliente frmMod = new FrmEditarCliente();

            if (dgvDatos.CurrentRow != null)
            {
                //si esta seleccionado algun dato obtenemos el id del dato para pasar al otro formulrion, para su respectiva edicion, en el otro form el texbox, su popiedad  modifier esta en publico, no es la manera mas correcta de hacerlo pero el mas facil
                frmMod.txtId.Text = dgvDatos.CurrentRow.Cells["Id"].Value.ToString();
                frmMod.txtRuc.Text = dgvDatos.CurrentRow.Cells["Ruc"].Value.ToString();
                frmMod.txtDni.Text = dgvDatos.CurrentRow.Cells["Dni"].Value.ToString();
                frmMod.txtNombre.Text = dgvDatos.CurrentRow.Cells["Nombre"].Value.ToString();
                frmMod.txtApellidoPaterno.Text = dgvDatos.CurrentRow.Cells["ApellidoPaterno"].Value.ToString();
                frmMod.txtApellidoMaterno.Text = dgvDatos.CurrentRow.Cells["ApellidoMaterno"].Value.ToString();
                frmMod.txtEmail.Text = dgvDatos.CurrentRow.Cells["Email"].Value.ToString();
                frmMod.txtTelefono.Text = dgvDatos.CurrentRow.Cells["Telefono"].Value.ToString();
                frmMod.txtCelular.Text = dgvDatos.CurrentRow.Cells["Celular"].Value.ToString();
                frmMod.txtDireccion.Text = dgvDatos.CurrentRow.Cells["Direccion"].Value.ToString();
                frmMod.dtpFechaRegistro.Text = dgvDatos.CurrentRow.Cells["FechaRegistro"].Value.ToString();

                frmMod.txtDni.Text = dgvDatos.CurrentRow.Cells["Dni"].Value.ToString();

                fila = dgvDatos.CurrentCellAddress.X; // obtenemos el indice de la fila seleccionada
                columna = dgvDatos.CurrentCellAddress.Y;// obtenemos el indice de la columna seleccionada
            }

            DialogResult res = frmMod.ShowDialog(); //mostramos el formulario nuevo como dialogo modal, para cuando se cierre hacer una accion en el form princiapal

            FrmClientes_Load(null, null);// se cierra el anterior formulario de editar y recargamos el formularioCategoria para actualizar datos modificados
            dgvDatos.CurrentCell = dgvDatos.Rows[columna].Cells[fila];
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Está seguro de eliminar el dato seleccionados?", "Eliminando...",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    Cliente cliente = new Cliente();// instanciamo nuestra clase para rellenarle sus atributos
                    cliente.Id = Convert.ToInt32(dgvDatos.CurrentRow.Cells["Id"].Value.ToString());// para eliminar solo le pasaremos su atributo id

                    if (FCliente.Eliminar(cliente) > 0)// Ejecutamos el procedimiento almacenado para eliminar, si se pudo eliminar... recargamos el formulario
                    {
                        FrmClientes_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar, es más probable que tenga ventas asociadas a este cliente \nPrimero deberá eliminar todas las ventas a este cliente", "No se puede eliminar");

                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.StackTrace);
                MessageBox.Show("No se pudo eliminar, es más probable que tenga ventas asociadas a este cliente \nPrimero deberá eliminar todas las ventas a este cliente", "No se puede eliminar");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            FrmClientes_Load(null, null);
        }

        //3p esto es para seleccionar un producto desde ventas
        internal void InsertarCliente(string valor)
        {
            txtInsertarCliente.Text = valor;
        }
        //4p Esto es para pasar datos a las ventas... el 5p se encuentra en FrmVentas
        private void pasarDatosAVentas()
        {
            if (txtInsertarCliente.Text == "1")
            {
                FrmVentasAsistente formulario = FrmVentasAsistente.InstanciaUnica();
                if (dgvDatos.CurrentRow != null)
                {
                    formulario.InsertarProducto(dgvDatos.CurrentRow.Cells["Id"].Value.ToString(), dgvDatos.CurrentRow.Cells["Nombre"].Value.ToString(), dgvDatos.CurrentRow.Cells["Direccion"].Value.ToString(), dgvDatos.CurrentRow.Cells["Ruc"].Value.ToString());
                    formulario.Show();
                    Close();
                }
            }
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //6p pasar datos en si al form FrmVentas 
            pasarDatosAVentas();
        }
    }
}
