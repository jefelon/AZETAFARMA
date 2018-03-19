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

namespace SistemaVentas.Presentacion.Compras
{
    public partial class FrmCompras : Form
    {
        public FrmCompras()
        {
            InitializeComponent();
        }
        private static DataTable dt = new DataTable();
        private void FrmCompras_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = FCompra.GetAll();
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmComprasAsistente compras = FrmComprasAsistente.InstanciaUnica();
            compras.Show();
        }
        int fila;
        int columna;

        private void btnEditar_Click(object sender, EventArgs e)
        {
            FrmComprasAsistente frmMod = FrmComprasAsistente.InstanciaUnica();

            if (dgvDatos.CurrentRow != null)
            {
                frmMod.lblCompraId.Text = dgvDatos.CurrentRow.Cells["Id"].Value.ToString();
                frmMod.txtNumeroDocumento.Text = dgvDatos.CurrentRow.Cells["NumeroDocumento"].Value.ToString();
                frmMod.txtNombreProveedor.Text = dgvDatos.CurrentRow.Cells["NombreProveedor"].Value.ToString();
                frmMod.txtDireccionProveedor.Text = dgvDatos.CurrentRow.Cells["Direccion"].Value.ToString();
                frmMod.txtIdProveedor.Text = dgvDatos.CurrentRow.Cells["ProveedorId"].Value.ToString();
                frmMod.txtRucProveedor.Text = dgvDatos.CurrentRow.Cells["Ruc"].Value.ToString();
                frmMod.dtpFecha.Text = dgvDatos.CurrentRow.Cells["Fecha"].Value.ToString();
                //el tipo d edocumento lo cargamos en el FrmComprasAsistente

                fila = dgvDatos.CurrentCellAddress.X;
                columna = dgvDatos.CurrentCellAddress.Y;
            }

            DialogResult res = frmMod.ShowDialog(); 

            FrmCompras_Load(null, null);
            dgvDatos.CurrentCell = dgvDatos.Rows[columna].Cells[fila];
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Está seguro de eliminar el dato seleccionados?", "Eliminando...",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    Compra compra = new Compra();
                    compra.Id = Convert.ToInt32(dgvDatos.CurrentRow.Cells["Id"].Value.ToString());

                    if (FCompra.Eliminar(compra) > 0)
                    {
                        FrmCompras_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar, la compra que quiere eliminar tiene items \n Haga click en el boton editar y elimine primero sus items y despues intente borrar nuevamente", "No se puede eliminar");

                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.StackTrace);
                MessageBox.Show("No se pudo eliminar, la compra que quiere eliminar tiene items \n Haga click en el boton editar y elimine primero sus items y despues intente borrar nuevamente", "No se puede eliminar");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            FrmCompras_Load(null, null);
        }
    }
}
