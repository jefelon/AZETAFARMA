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

namespace SistemaVentas.Presentacion.Clientes
{
    public partial class FrmEditarCliente : Form
    {
        public FrmEditarCliente()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            { string sResultado = validarDatos();
                if (sResultado == "") //todo bien
                {
                    if (txtNombre.Text != "")
                    {
                        Cliente cliente = new Cliente();
                        cliente.Id = Convert.ToInt32(txtId.Text);
                        cliente.Ruc = txtRuc.Text;
                        cliente.Dni = txtDni.Text;
                        cliente.Nombre = txtNombre.Text;
                        cliente.ApellidoPaterno = txtApellidoPaterno.Text;
                        cliente.ApellidoMaterno = txtApellidoMaterno.Text;
                        cliente.Email = txtEmail.Text;
                        cliente.Telefono = txtTelefono.Text;
                        cliente.Celular = txtCelular.Text;
                        cliente.Direccion = txtDireccion.Text;
                        cliente.FechaRegistro = dtpFechaRegistro.Value;

                        if (FCliente.Actualizar(cliente) > 0)
                        {
                            MessageBox.Show(txtNombre.Text + " se modificó correctamente");
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
            if (txtRuc.Text.Length < 11 || txtRuc.Text.Length > 11)
            {
                resultado = "El campo RUC tiene pocos o demasiados dígitos";
                txtRuc.Focus();
            }

            if (txtDni.Text.Length < 8 || txtDni.Text.Length > 8)
            {
                resultado = "El campo DNI tiene pocos o demasiados dígitos";
                txtDni.Focus();
            }

            if (txtNombre.Text == "")
            {
                resultado = "El nombre está vacio";
                txtNombre.Focus();
            }
            return resultado;
        }
    }
}
