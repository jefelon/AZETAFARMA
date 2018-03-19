using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SistemaVentas.Entidad;
using SistemaVentas.Datos;

namespace SistemaVentas.Presentacion.Categorias
{
    public partial class FrmAgregarCategoria : Form
    {
        public FrmAgregarCategoria()
        {
            InitializeComponent();
        }
        private void FrmAgregarCategoria_Load(object sender, EventArgs e)
        {

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
                        Categoria categoria = new Categoria();
                        categoria.Descripcion = txtDescripcion.Text;

                        int returnId = FCategoria.Insertar(categoria);
                        if (returnId > 0)
                        {
                            MessageBox.Show("Los datos se agregaron correctamente");
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
            return resultado;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
