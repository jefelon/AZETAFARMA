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
    public partial class FrmDetalleProductos : Form
    {
        public FrmDetalleProductos()
        {
            InitializeComponent();
        }

        public string cantidad;
        public string ventaId;
        public string productoId;

        private static DataTable dt = new DataTable();
        private void FrmDetalleProductos_Load(object sender, EventArgs e)
        {
            try
            {
                lblVentaId.Text = ventaId;
                lblProductoId.Text = productoId;

                DataSet ds = FProducto.Get(Convert.ToInt32(lblProductoId.Text));
                dt = ds.Tables[0];

                lblCodigo.Text = dt.Rows[0]["Codigo"].ToString();
                lblNombreProducto.Text = dt.Rows[0]["Nombre"].ToString();
                lblCategoriaProducto.Text = dt.Rows[0]["Categoria"].ToString();
                txtPrecioUnitario.Text = dt.Rows[0]["PrecioVenta"].ToString();
                txtStock.Text = dt.Rows[0]["Stock"].ToString();
                lblPresentacion.Text = dt.Rows[0]["Presentacion"].ToString();
                lblLaboratorio.Text = dt.Rows[0]["Laboratorio"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    agregarItem();
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            
        }

        private void agregarItem()
        {
            try
            {
                string sResultado = ValidarDatos();

                if (sResultado == "")
                {

                    DetalleVenta detalleVenta = new DetalleVenta();
                    detalleVenta.Venta.Id = Convert.ToInt32(lblVentaId.Text);
                    detalleVenta.Producto.Id = Convert.ToInt32(lblProductoId.Text);
                    detalleVenta.Cantidad = Convert.ToDouble(txtCantidad.Text);
                    detalleVenta.PrecioUnitario = Convert.ToDouble(txtPrecioUnitario.Text);



                    if (Convert.ToDouble(txtCantidad.Text) > Convert.ToDouble(txtStock.Text) || Convert.ToDouble(txtStock.Text) <= 0)
                    {
                        MessageBox.Show("La cantidad que quiere vender supera el stock... O no hay stock suficiente \n Ingrese más Stock al producto e intente nuevamente.");

                    }
                    else
                    {
                        int iDetVentaId = FDetalleVenta.Insertar(detalleVenta);
                    }
                }
                else
                {
                    MessageBox.Show(sResultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        public string ValidarDatos()
        {
            string resultado = "";
            //if (txtNombre.Text == "")
            //{
            //    resultado = "El nombre está vacio";
            //    txtNombre.Focus();
            //}
            return resultado;
        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            agregarItem();
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
