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

namespace SistemaVentas.Presentacion.Inventarios
{
    public partial class FrmAgregarInventario : Form
    {
        public FrmAgregarInventario()
        {
            InitializeComponent();
        }

        private void FrmAgregarInventario_Load(object sender, EventArgs e)
        {
            txtUsuario.Text = Usuario.NombreUsuario;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string sResultado = validarDatos();
                if (sResultado == "") //todo bien
                {       //Id, Fecha, UsuarioId, Descripcion
                        Inventario inventario = new Inventario();
                        inventario.Fecha = dtpFecha.Value;
                        inventario.UsuarioId = Usuario.Id;
                        inventario.Descripcion = txtDescripcion.Text;

                        int returnId = FInventario.Insertar(inventario);
                        if (returnId > 0)
                        {
                            MessageBox.Show("Los datos se agregaron correctamente");
                            Close();
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
            if (txtUsuario.Text == "")
            {
                resultado = "El usuario no puede estar vacío";
                txtDescripcion.Focus();
            }
            if (txtDescripcion.Text == "")
            {
                resultado = "Describa el motivo del inventario";
                txtDescripcion.Focus();
            }

            return resultado;
        }
    }
}
