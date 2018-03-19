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

namespace SistemaVentas.Presentacion.Proveedores
{
    public partial class FrmEditarProveedores : Form
    {
        public FrmEditarProveedores()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string sResultado = validarDatos();
                if (sResultado == "") //todo bien
                {
                    if (txtNombre.Text != "")
                    {
                        Proveedor proveedor = new Proveedor();
                        proveedor.Id = Convert.ToInt32(txtId.Text);
                        proveedor.Nombre = txtNombre.Text;
                        proveedor.Ruc = txtRuc.Text;
                        proveedor.Direccion = txtDireccion.Text;
                        proveedor.Telefono = txtTelefono.Text;
                        proveedor.Email = txtEmail.Text;
                        proveedor.FechaRegistro = dtpFechaRegistro.Value;

                        int returnId = FProveedor.Actualizar(proveedor);
                        if (returnId > 0)
                        {
                            MessageBox.Show("Los datos se modificaron correctamente");
                            Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error: \n" + sResultado);
                }//fin validacion datos
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        public string validarDatos()
        {
            string resultado = "";
            if (txtNombre.Text == "")
            {
                resultado = "El nombre está vacio";
                txtNombre.Focus();
            }
            if (txtRuc.Text.Length < 11 || txtRuc.Text.Length > 11)
            {
                resultado = "El Campo RUC tiene pocos digiTos o demasiados";
                txtRuc.Focus();
            }
            if (txtTelefono.Text.Length < 8 || txtTelefono.Text.Length > 8)
            {
                resultado = "El Campo TELEFONO tiene pocos digiTos o demasiados";
                txtTelefono.Focus();
            }
            return resultado;
        }
    }
}
