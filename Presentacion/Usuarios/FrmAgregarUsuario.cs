using SistemaVentas.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SistemaVentas.Presentacion.Usuarios
{
    public partial class FrmAgregarUsuario : Form
    {
        public FrmAgregarUsuario()
        {
            InitializeComponent();
        }
        private void FrmAgregarUsuario_Load(object sender, EventArgs e)
        {

        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contrasena = txtContrasena.Text;
            string hash = FUsuario.Helper.EncodePassword(string.Concat(usuario, contrasena));
            try
            {
                string sResultado = validarDatos();
                if (sResultado == "") //todo bien
                {                                      
                    if (FUsuario.Insertar(usuario, hash, txtNombre.Text, txtApellidoPaterno.Text,txtApellidoMaterno.Text, txtEmail.Text,txtTelefono.Text, txtCelular.Text,txtDireccion.Text,txtDni.Text,txtRol.Text) > 0)
                      {
                            MessageBox.Show(txtNombre.Text + " se insertó correctamente");
                      } 
                }
                else
                {
                    MessageBox.Show("Faltan Datos para \n" + sResultado);
                }//fin validacion datos
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private string validarDatos()
        {
            string resultado = "";
            if (txtUsuario.Text == "")
            {
                resultado = "-El usuario es obligatorio.";
                txtUsuario.Focus();
                txtUsuario.Focus();
            }
            if (txtNombre.Text == "")
            {
                resultado = "-El nombre es obligatorio.";
                txtNombre.Focus();
            }
            if (txtContrasena.Text == "")
            {
                resultado = "-La contraseña no puede ser vacío.";
                txtContrasena.Focus();
            }
            if (txtDni.Text.Length < 8 || txtDni.Text.Length > 8)
            {
                resultado = "-El DNI tiene pocos caracteres o demasiados.";
                txtDni.Focus();
            }
            return resultado;
        
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
