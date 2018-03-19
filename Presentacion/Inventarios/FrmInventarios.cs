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

namespace SistemaVentas.Presentacion.Inventarios
{
    public partial class FrmInventarios : Form
    {
        public FrmInventarios()
        {
            InitializeComponent();
        }

        private static DataTable dt = new DataTable();
        private void FrmInventarios_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = FInventario.GetAll();//creamos un dataset, un contenedor de datos y hacemos una consulta con la clase FCategoria y su método GetAll
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
            FrmAgregarInventario add = new FrmAgregarInventario();
            DialogResult res = add.ShowDialog();

            FrmInventarios_Load(null, null);
        }


        int fila;
        int columna;
        private void btnEditar_Click(object sender, EventArgs e)
        {

            if (dgvDatos.CurrentRow != null)
            {
                FrmEditarInventario frmMod = new FrmEditarInventario();
                
                frmMod.txtId.Text = dgvDatos.CurrentRow.Cells["Id"].Value.ToString();
                frmMod.dtpFecha.Text = dgvDatos.CurrentRow.Cells["Fecha"].Value.ToString();
                frmMod.txtUsuario.Text = dgvDatos.CurrentRow.Cells["Nombre"].Value.ToString();
                frmMod.txtDescripcion.Text = dgvDatos.CurrentRow.Cells["Descripcion"].Value.ToString();

                fila = dgvDatos.CurrentCellAddress.X; 
                columna = dgvDatos.CurrentCellAddress.Y;
                DialogResult res = frmMod.ShowDialog(); 
            }

            FrmInventarios_Load(null, null);
            dgvDatos.CurrentCell = dgvDatos.Rows[columna].Cells[fila];
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Está seguro de eliminar el dato seleccionados?", "Eliminando...",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    Inventario inventario = new Inventario();
                    inventario.Id = Convert.ToInt32(dgvDatos.CurrentRow.Cells["Id"].Value.ToString());

                    if (FInventario.Eliminar(inventario) > 0)
                    {
                        FrmInventarios_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar, talvez hay productos inventariados, primero borrelos y despues intente nuevamente", "No se puede eliminar");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar, talvez hay productos inventariados, primero borrelos y despues intente nuevamente", "No se puede eliminar");
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (dgvDatos.CurrentRow != null)
            {
                FrmAsistenteTomaInventario frmMod = new FrmAsistenteTomaInventario();

                frmMod.txtInventarioId.Text = dgvDatos.CurrentRow.Cells["Id"].Value.ToString();


                fila = dgvDatos.CurrentCellAddress.X;
                columna = dgvDatos.CurrentCellAddress.Y;
                DialogResult res = frmMod.ShowDialog();
            }

            FrmInventarios_Load(null, null);
            dgvDatos.CurrentCell = dgvDatos.Rows[columna].Cells[fila];
        }
    }
}
