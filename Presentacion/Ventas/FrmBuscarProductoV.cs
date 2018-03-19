using SistemaVentas.Datos;
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
    public partial class FrmBuscarProducto : Form
    {
        public FrmBuscarProducto()
        {
            InitializeComponent();
        }

        //private static DataTable dt = new DataTable();
        public string ventaid;
        public string productoId;
        private void txtBuscarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;

                    if(rbtGeneral.Checked==true)
                    {
                        DataSet ds = FProducto.Buscar(txtBuscarProducto.Text);
                        DataTable dt = ds.Tables[0];
                    

                        if (dt.Rows.Count <= 0)
                        {
                            MessageBox.Show("No hay resultados para su criterio de busqueda, intente nuévamente","No hay resultados");
                            dgvListaProductos.DataSource = null;
                        }
                        else if (dt.Rows.Count > 0)
                        
                        {
                          dgvListaProductos.DataSource = dt;
                          dgvListaProductos.Columns["ProductoId"].Visible = false;
                          dgvListaProductos.Columns["Codigo"].Width = 45;
                          dgvListaProductos.Columns["Nombre"].Width = 240;
                          dgvListaProductos.Columns["PrecioVenta"].Width = 40;
                          dgvListaProductos.Columns["PrecioVenta"].HeaderText = "P.V.";
                          dgvListaProductos.Columns["Stock"].Width = 45;
                          dgvListaProductos.Columns["Composicion"].Width = 240;
                          dgvListaProductos.Columns["Indicacion"].Visible = false;
                          dgvListaProductos.Focus();

                          txtIndicacion.Text=dgvListaProductos.CurrentRow.Cells["Indicacion"].Value.ToString();
                      
                        }
                    }
                    if (rbtIndicacion.Checked == true)
                    {
                        DataSet ds = FProducto.BuscarPorIndicacion(txtBuscarProducto.Text);
                        DataTable dt = ds.Tables[0];


                        if (dt.Rows.Count <= 0)
                        {
                            MessageBox.Show("No hay resultados para su criterio de busqueda, intente nuévamente", "No hay resultados");
                            dgvListaProductos.DataSource = null;
                        }
                        else if (dt.Rows.Count > 0)
                        {
                            dgvListaProductos.DataSource = dt;
                            dgvListaProductos.Columns["ProductoId"].Visible = false;
                            dgvListaProductos.Columns["Codigo"].Width = 45;
                            dgvListaProductos.Columns["Nombre"].Width = 240;
                            dgvListaProductos.Columns["PrecioVenta"].Width = 40;
                            dgvListaProductos.Columns["PrecioVenta"].HeaderText = "P.V.";
                            dgvListaProductos.Columns["Stock"].Width = 45;
                            dgvListaProductos.Columns["Composicion"].Width = 240;
                            dgvListaProductos.Columns["Indicacion"].Visible = false;
                            dgvListaProductos.Focus();

                            txtIndicacion.Text = dgvListaProductos.CurrentRow.Cells["Indicacion"].Value.ToString();

                        }
                    }
                }
                if (e.KeyCode == Keys.Tab)
                {
                    e.SuppressKeyPress = true;
                    dgvListaProductos.Focus();
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
                FrmDetalleProductos detalleproducto = new FrmDetalleProductos();
                detalleproducto.ventaId = lblVentaId.Text;
                detalleproducto.productoId = dgvListaProductos.CurrentRow.Cells["ProductoId"].Value.ToString();
               DialogResult res= detalleproducto.ShowDialog();
               txtBuscarProducto.Focus();
            }
        }
        private void dgvListaProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            pasarDatosADetalle();
        }

        private void FrmBuscarProducto_Load(object sender, EventArgs e)
        {
            lblVentaId.Text= ventaid;
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
                if (e.KeyCode == Keys.Tab)
                {
                    e.SuppressKeyPress = true;
                    dgvProductosRelacionados.Focus();
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void FrmBuscarProducto_KeyDown(object sender, KeyEventArgs e)
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

        private void dgvListaProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cargarProductosRelacionado();
        }

        private void cargarProductosRelacionado()
        {
            try
            {
                
                    DataSet ds = FProducto.BuscarMismaComposicion(dgvListaProductos.CurrentRow.Cells["Composicion"].Value.ToString());
                    DataTable dt = ds.Tables[0];

                    DataSet dataSet = FProducto.BuscarDistintoLaboratorio(txtBuscarProducto.Text, dgvListaProductos.CurrentRow.Cells["Laboratorio"].Value.ToString());
                    DataTable dtable = dataSet.Tables[0];


                    if (dt.Rows.Count >= 0)
                    {
                        dgvProductosRelacionados.DataSource = dtable;
                        txtIndicacion.Text = dgvProductosRelacionados.CurrentRow.Cells["Indicacion"].Value.ToString();
                        ocultarAjustarColumnas();

                    }
                    else if (dt.Rows.Count > 0)
                    {
                        dgvProductosRelacionados.DataSource = dt;
                        txtIndicacion.Text = dgvProductosRelacionados.CurrentRow.Cells["Indicacion"].Value.ToString();
                        ocultarAjustarColumnas();
                    }
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void ocultarAjustarColumnas()
        {
            dgvProductosRelacionados.Columns["ProductoId"].Visible = false;
            dgvProductosRelacionados.Columns["Codigo"].Width = 45;
            dgvProductosRelacionados.Columns["Nombre"].Width = 240;
            dgvProductosRelacionados.Columns["PrecioVenta"].Width = 40;
            dgvProductosRelacionados.Columns["PrecioVenta"].HeaderText = "P.V.";
            dgvProductosRelacionados.Columns["Stock"].Width = 45;
            dgvProductosRelacionados.Columns["Composicion"].Width = 240;
            dgvProductosRelacionados.Columns["Indicacion"].Visible = false;
        }

        private void dgvProductosRelacionados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            pasarDatosADetalle();
        }

        private void dgvProductosRelacionados_KeyDown(object sender, KeyEventArgs e)
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

        private void dgvListaProductos_SelectionChanged(object sender, EventArgs e)
        {
            cargarProductosRelacionado();
        }

        private void dgvProductosRelacionados_SelectionChanged(object sender, EventArgs e)
        {

        }
    }
}
