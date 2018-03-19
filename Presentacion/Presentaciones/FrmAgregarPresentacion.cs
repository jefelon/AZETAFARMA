using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SistemaVentas.Datos;
using SistemaVentas.Entidad;

namespace SistemaVentas.Presentacion.Presentaciones
{
    public partial class FrmAgregarPresentacion : Form
    {
        public FrmAgregarPresentacion()
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
                    if (txtDescripcion.Text != "")
                    {
                        Presentacion_ presentacion = new Presentacion_();
                        presentacion.Descripcion = txtDescripcion.Text;

                        int returnId = FPresentacion_.Insertar(presentacion);
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
            if (txtDescripcion.Text == "")
            {
                resultado = "La Descripcion no puede estar vacio.";
                txtDescripcion.Focus();
            }
            return resultado;
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
