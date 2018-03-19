using FastReport;
using SistemaVentas.Datos;
using SistemaVentas.Entidad;
using SisttVentas;
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
    public partial class FrmAsistenteTomaInventario : Form
    {
        public FrmAsistenteTomaInventario()
        {
            InitializeComponent();
        }

        private static DataTable dt = new DataTable();
        private void FrmAsistenteTomaInventario_Load(object sender, EventArgs e)
        {
           
            try
            {
                DataSet ds = FDetalleInventario.Get(Convert.ToInt32(txtInventarioId.Text));
                dt = ds.Tables[0]; 
                dgvItems.DataSource = dt;

                dgvItems.Columns["Id"].Width = 40;
                dgvItems.Columns["ProductoId"].Visible = false;
                dgvItems.Columns["Codigo"].Width = 60;
                dgvItems.Columns["Nombre"].Width = 200;

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

        private int id;
        private int productoId;
        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                try
                {
                    DataSet ds = FDetalleInventario.BuscarDetalleInventario(txtCodigo.Text,Convert.ToInt32(txtInventarioId.Text));
                    DataTable dt = ds.Tables[0];
                     if (dt.Rows.Count >0)
                    {
                        // hay detalle del inventario del producto
                         productoId = Convert.ToInt32(dt.Rows[0]["ProductoId"].ToString());
                         id = Convert.ToInt32(dt.Rows[0]["Id"].ToString());

                        DetalleInventario detalleInventarios = new DetalleInventario();
                        detalleInventarios.Id = id;
                        detalleInventarios.Producto.Id = productoId;
                        detalleInventarios.CantidadContada = 1;
                       
                        int iActualizar = FDetalleInventario.Actualizar(detalleInventarios);//si se actualizo corectamente sumamos uno a la cantidad contada, es por que es lector de código, en cada pasada cuenta uno
                        if (iActualizar > 0)
                        {
                            FrmAsistenteTomaInventario_Load(null, null);
                        }
                    }
                     if (dt.Rows.Count <= 0)
                     {
                            //Id, InventarioId, ProductoId, CantidadContada, CantidadSistema
                            DetalleInventario detalleInventario = new DetalleInventario();
                            detalleInventario.Inventario.Id = Convert.ToInt32(txtInventarioId.Text);
                            detalleInventario.Producto.Codigo = txtCodigo.Text;
                            detalleInventario.CantidadContada = 1;
                           
                            int iInsertar = FDetalleInventario.Insertar(detalleInventario);

                            if (iInsertar > 0)
                            {
                                FrmAsistenteTomaInventario_Load(null, null);
                            }
                     }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
        }
        int fila;
        int columna;
        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FrmEditarItemDetalleInventario edit = new FrmEditarItemDetalleInventario();
                edit.lblDetalleInventarioId.Text = dgvItems.CurrentRow.Cells["Id"].Value.ToString();
                edit.lblCodigo.Text = dgvItems.CurrentRow.Cells["Codigo"].Value.ToString();
                edit.txtNombreProducto.Text = dgvItems.CurrentRow.Cells["Nombre"].Value.ToString();
                edit.txtCantidadContada.Text = dgvItems.CurrentRow.Cells["CantidadContada"].Value.ToString();
          
                fila = dgvItems.CurrentCellAddress.X; // obtenemos el indice de la fila seleccionada
                columna = dgvItems.CurrentCellAddress.Y;

            DialogResult res = edit.ShowDialog();

            FrmAsistenteTomaInventario_Load(null, null);
            dgvItems.CurrentCell = dgvItems.Rows[columna].Cells[fila];

        }

        private void dgvItems_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    e.SuppressKeyPress = true;
                    DetalleInventario detalleInventario = new DetalleInventario();
                    detalleInventario.Id = Convert.ToInt32(dgvItems.CurrentRow.Cells["Id"].Value.ToString());

                    int iEliminar=FDetalleInventario.Eliminar(detalleInventario);

                    if (iEliminar > 0)
                    {
                        FrmAsistenteTomaInventario_Load(null,null);
                    }
                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Report rptComprobantes = new Report();
            EditarAppConfig leerConfig = new EditarAppConfig();
            rptComprobantes.Load(@"Reportes/Inventario.frx");
            rptComprobantes.SetParameterValue("InventarioId", Convert.ToInt32(txtInventarioId.Text));
            rptComprobantes.Dictionary.Connections[0].ConnectionString = leerConfig.leerValorConfiguracion("connectionString");
            rptComprobantes.Show();
        }

    }
}
