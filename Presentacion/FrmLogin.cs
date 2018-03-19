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

namespace SistemaVentas.Presentacion
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void login()
        {
            try
            {
                string usuario = txtUsuario.Text.Trim();
                string contrasena = txtContrasena.Text.Trim();
                string hash = FUsuario.Helper.EncodePassword(string.Concat(usuario, contrasena));

                DataSet ds = FUsuario.Autenticar(usuario, hash);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("El usuario no existe. Digitar correctamente el usuario y/o contraseña.", "ERROR DE INICIO DE SECION");
                }
                if (dt.Rows.Count > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    Usuario.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                    Usuario.NombreUsuario = dt.Rows[0]["Usuario"].ToString();
                    Usuario.Contrasena = dt.Rows[0]["Contrasena"].ToString();
                    Usuario.Nombre = dt.Rows[0]["Nombre"].ToString();
                    Usuario.ApellidoPaterno = dt.Rows[0]["ApellidoPaterno"].ToString();
                    Usuario.ApellidoMaterno = dt.Rows[0]["ApellidoMaterno"].ToString();
                    Usuario.Telefono = dt.Rows[0]["Telefono"].ToString();
                    Usuario.Celular = dt.Rows[0]["Celular"].ToString();
                    Usuario.Direccion = dt.Rows[0]["Direccion"].ToString();
                    Usuario.Dni = dt.Rows[0]["Dni"].ToString();
                    Usuario.Rol = dt.Rows[0]["Rol"].ToString();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

            //else{
            //    this.DialogResult = DialogResult.Abort;
            //}
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
           login();

        }

        private void txtContrasena_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }
        }

    }
}
