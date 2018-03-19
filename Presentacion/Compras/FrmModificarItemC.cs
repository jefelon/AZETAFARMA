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
    public partial class FrmModificarItemC : Form
    {
        public FrmModificarItemC()
        {
            InitializeComponent();
        }
        public int ventaId;
        public int productoId;
        public double precioCosto;
        private void FrmModificarItemC_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void guardarItemModificado()
        {
            //DetalleVenta detalleVenta = new DetalleVenta();
            //detalleVenta.Id = Convert.ToInt32(lblDetalleVentaId.Text);
            //detalleVenta.Venta.Id = ventaId;
            //detalleVenta.Producto.Id = productoId;
            //detalleVenta.Cantidad = Convert.ToDouble(txtCantidad.Text);
            //detalleVenta.PrecioUnitario = precioUnitario;
            //FDetalleVenta.Actualizar(detalleVenta);
            //Close();

            // se ha creado otro procedimiento alamacenado que solo modifica la cantidad "[dbo].[usp_Datos_FDetalleVenta_ModificarItem]" puesto que al convertir los datos del datagridview pueden afectar a errores de cálculo por el separador decimal.
            //recuerdar que no se actualiza el stock en ningun lado del form, ese punto está con triguers en a tabla FrmDetalleCompra--> Desencadenadores
            DetalleCompra detalleCompra = new DetalleCompra();
            detalleCompra.Id = Convert.ToInt32(lblDetalleCompraId.Text);
            detalleCompra.Cantidad = Convert.ToDouble(txtCantidad.Text);

            FDetalleCompra.ModificarItem(detalleCompra);   
            Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                guardarItemModificado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                guardarItemModificado();
            }  
        }

    }
}
