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
    public partial class FrmDetalleProductoC : Form
    {
        public FrmDetalleProductoC()
        {
            InitializeComponent();
        }

        public string cantidad;
        public string compraId;
        public string productoId;

        private static DataTable dt = new DataTable();
        double porcentajeGanancia;
        private void FrmDetalleProductoC_Load(object sender, EventArgs e)
        {
            try
            {   
                lblCompraId.Text = compraId;
                lblProductoId.Text = productoId;

                DataSet ds = FProducto.Get(Convert.ToInt32(lblProductoId.Text));
                dt = ds.Tables[0];

                lblCodigo.Text = dt.Rows[0]["Codigo"].ToString();
                lblNombreProducto.Text = dt.Rows[0]["Nombre"].ToString();
                txtPrecioCosto.Text = dt.Rows[0]["PrecioCompra"].ToString();
                txtPrecioVenta.Text = dt.Rows[0]["PrecioVenta"].ToString();
                txtStock.Text = dt.Rows[0]["Stock"].ToString();
                txtPrecioCosto.Focus();
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
                    DetalleCompra detalleCompra = new DetalleCompra();
                    detalleCompra.Compra.Id = Convert.ToInt32(lblCompraId.Text);
                    detalleCompra.Producto.Id = Convert.ToInt32(lblProductoId.Text);
                    detalleCompra.Cantidad = Convert.ToDouble(txtCantidad.Text);
                    detalleCompra.PrecioCosto = Convert.ToDouble(txtPrecioCosto.Text);

                    if (Convert.ToDouble(txtCantidad.Text) > 0)
                    {
                        int iDetCompraId = FDetalleCompra.Insertar(detalleCompra,Convert.ToDouble(txtPorcentajeGanancia.Text));
                        Close();
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
            if (txtCantidad.Text == "")
            {
                resultado = "La cantidad debe ser mayor a cero";
                txtCantidad.Focus();
            }


            if (txtPorcentajeGanancia.Text == "")
            {
                resultado = "El porcentaje de ganancia debe ser igual o mayor a cero";
                txtPorcentajeGanancia.Focus();
            }
            
            if (txtPrecioCosto.Text == "")
            {
                resultado = "Registrar el precio de coto";
                txtPrecioCosto.Focus();
            }
            return resultado;
        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            agregarItem();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtPorcentajeGanancia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtPrecioCosto.Text))
                {
                    double precioCompra = Convert.ToDouble(txtPrecioCosto.Text);
                    double porcentaje = Convert.ToDouble(txtPorcentajeGanancia.Text); ;
                    double precioventa = precioCompra + (precioCompra * porcentaje) / 100;

                    txtPrecioVenta.Text = Convert.ToString(precioventa);
                }
            }
            catch (Exception ex)
            {

               // MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void txtPrecioCosto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtPorcentajeGanancia.Text))
                {
                    double precioCompra = Convert.ToDouble(txtPrecioCosto.Text);
                    double porcentaje = Convert.ToDouble(txtPorcentajeGanancia.Text); ;
                    double precioventa = precioCompra + (precioCompra * porcentaje) / 100;

                    txtPrecioVenta.Text = Convert.ToString(precioventa);
                }
            }
            catch (Exception ex)
            {

                // MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

    }
}
