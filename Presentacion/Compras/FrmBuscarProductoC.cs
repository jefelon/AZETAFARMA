using SistemaVentas.Datos;
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
    public partial class FrmBuscarProductoC : Form
    {
        public FrmBuscarProductoC()
        {
            InitializeComponent();
        }

        private static DataTable dt = new DataTable();
        public string compraid;
        public string productoId;
        private void FrmBuscarProductoC_Load(object sender, EventArgs e)
        {
            lblCompraId.Text = compraid;
        }
        private void txtBuscarProductoC_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    DataSet ds = FProducto.Buscar(txtBuscarProducto.Text);
                    DataTable dt = ds.Tables[0];


                    if (dt.Rows.Count <= 0)
                    {
                        MessageBox.Show("No hay resultados para su criterio de busqueda, intente nuévamente", "No hay resultados");
                    }
                    else if (dt.Rows.Count > 0)
                    {
                        dgvListaProductos.DataSource = dt;
                        dgvListaProductos.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void pasarDatosADetalle()
        {
            if (dgvListaProductos.CurrentRow != null)
            {
                //si esta seleccionado algun dato obtenemos el id del dato para pasar al otro formulrion, para su respectiva edicion, en el otro form el texbox, su popiedad  modifier esta en publico, no es la manera mas correcta de hacerlo pero el mas facil
                FrmDetalleProductoC detalleproducto = new FrmDetalleProductoC();
                detalleproducto.compraId = lblCompraId.Text;
                detalleproducto.productoId = dgvListaProductos.CurrentRow.Cells["ProductoId"].Value.ToString();
                detalleproducto.ShowDialog();
                txtBuscarProducto.Focus();
            }
        }
        private void dgvListaProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            pasarDatosADetalle();
        }

        private void dgvListaProductos_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;

                    pasarDatosADetalle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void FrmBuscarProductoC_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }


    }
}
