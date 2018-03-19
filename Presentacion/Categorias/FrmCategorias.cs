using SistemaVentas.Datos;
using SistemaVentas.Entidad;
using SistemaVentas.Presentacion.Productos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SistemaVentas.Presentacion.Categorias
{
    public partial class FrmCategorias : Form
    {
        private static DataTable dt = new DataTable(); //variable para crear un tabla
        public FrmCategorias()
        {
            InitializeComponent();
        }

        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            //una consulta a la base de datos siempre trataremos de omar el control, por si pasa algun error y se cierre la aplicación
            try
            {
                DataSet ds = FCategoria.GetAll();//creamos un dataset, un contenedor de datos y hacemos una consulta con la clase FCategoria y su método GetAll
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
            FrmAgregarCategoria addcat = new FrmAgregarCategoria();
            DialogResult res = addcat.ShowDialog();

            FrmCategorias_Load(null, null);
        }

        int fila;
        int columna;
        private void btnEditar_Click(object sender, EventArgs e)
        {
            FrmEditarCategoria frmMod = new FrmEditarCategoria();

            if (dgvDatos.CurrentRow != null)
            {
                //si esta seleccionado algun dato obtenemos el id del dato para pasar al otro formulrion, para su respectiva edicion, en el otro form el texbox, su popiedad  modifier esta en publico, no es la manera mas correcta de hacerlo pero el mas facil
                frmMod.txtId.Text = dgvDatos.CurrentRow.Cells["Id"].Value.ToString();
                frmMod.txtNombre.Text = dgvDatos.CurrentRow.Cells["Nombre"].Value.ToString();
                frmMod.txtDescripcion.Text = dgvDatos.CurrentRow.Cells["Descripcion"].Value.ToString();

                fila = dgvDatos.CurrentCellAddress.X; // obtenemos el indice de la fila seleccionada
                columna = dgvDatos.CurrentCellAddress.Y;// obtenemos el indice de la columna seleccionada
            }

            DialogResult res = frmMod.ShowDialog(); //mostramos el formulario nuevo como dialogo modal, para cuando se cierre hacer una accion en el form princiapal

            FrmCategorias_Load(null, null);// se cierra el anterior formulario de editar y recargamos el formularioCategoria para actualizar datos modificados
            dgvDatos.CurrentCell = dgvDatos.Rows[columna].Cells[fila];//Volvemos a seleccionar la fila modificada, para que el cliente no se pierd que dato modificó
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Está seguro de eliminar el dato seleccionados?", "Eliminando...",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    Categoria categoria = new Categoria();// instanciamo nuestra clase para rellenarle sus atributos
                    categoria.Id = Convert.ToInt32(dgvDatos.CurrentRow.Cells["Id"].Value.ToString());// para eliminar solo le pasaremos su atributo id

                    if (FCategoria.Eliminar(categoria) > 0)// Ejecutamos el procedimiento almacenado para eliminar, si se pudo eliminar... recargamos el formulario
                    {
                        FrmCategorias_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar, talvez hay productos en esta categoría", "No se puede eliminar");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            FrmCategorias_Load(null, null);
        }

        internal void InserttarCat(string valor)
        {
            txtInsertarCat.Text = valor;
        }

        internal void EditarCat(string valor)
        {
            txtEditarCat.Text = valor;
        }

    }
}
