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

namespace SistemaVentas.Presentacion.VentasAsistente
{
    public partial class FrmVentas : Form
    {
        public FrmVentas()
        {
            InitializeComponent();
        }
        private static DataTable dt = new DataTable();
        private void FrmVentas_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = FVenta.GetAll();
                dt = ds.Tables[0]; 
                dgvDatos.DataSource = dt; 

                if (dt.Rows.Count > 0)
                {
                    lblSinDatos.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmVentasAsistente vender = FrmVentasAsistente.InstanciaUnica();
            vender.Show();
        }

        int fila;
        int columna;
        private void btnEditar_Click(object sender, EventArgs e)
        {
            FrmVentasAsistente frmMod = FrmVentasAsistente.InstanciaUnica();

            if (dgvDatos.CurrentRow != null)
            {
                //si esta seleccionado algun dato obtenemos el id del dato para pasar al otro formulrion, para su respectiva edicion, en el otro form el texbox, su popiedad  modifier esta en publico, no es la manera mas correcta de hacerlo pero el mas facil
                frmMod.lblVentaId.Text = dgvDatos.CurrentRow.Cells["Id"].Value.ToString();
                frmMod.txtNumeroDocumento.Text = dgvDatos.CurrentRow.Cells["NumeroDocumento"].Value.ToString();
                frmMod.txtNombreCliente.Text = dgvDatos.CurrentRow.Cells["NombreCliente"].Value.ToString();
                frmMod.txtDireccionCliente.Text = dgvDatos.CurrentRow.Cells["Direccion"].Value.ToString();
                frmMod.txtIdCliente.Text = dgvDatos.CurrentRow.Cells["ClienteId"].Value.ToString();
                frmMod.txtRucCliente.Text = dgvDatos.CurrentRow.Cells["Ruc"].Value.ToString();
                frmMod.dtpFecha.Text = dgvDatos.CurrentRow.Cells["Fecha"].Value.ToString();
                //el tipo d edocumento lo cargamos en el FrmVentasAsistente

                fila = dgvDatos.CurrentCellAddress.X; // obtenemos el indice de la fila seleccionada
                columna = dgvDatos.CurrentCellAddress.Y;// obtenemos el indice de la columna seleccionada
            }

            DialogResult res = frmMod.ShowDialog(); //mostramos el formulario nuevo como dialogo modal, para cuando se cierre hacer una accion en el form princiapal

            FrmVentas_Load(null, null);// se cierra el anterior formulario de editar y recargamos el formularioCategoria para actualizar datos modificados
            dgvDatos.CurrentCell = dgvDatos.Rows[columna].Cells[fila];
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Está seguro de eliminar el dato seleccionados?", "Eliminando...",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    Venta venta = new Venta();
                    venta.Id = Convert.ToInt32(dgvDatos.CurrentRow.Cells["Id"].Value.ToString());

                    if (FVenta.Eliminar(venta) > 0)
                    {
                        FrmVentas_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar, talvez hay productos vendidos en esta venta \n Haga click en el boton editar y elimine primero sus items y despues intente borrar nuevamente", "No se puede eliminar");

                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.StackTrace);
                MessageBox.Show("No se pudo eliminar, talvez hay productos vendidos en esta venta \n Haga click en el boton editar y elimine primero sus items y despues intente borrar nuevamente", "No se puede eliminar");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            FrmVentas_Load(null,null);
        }


    }
}
