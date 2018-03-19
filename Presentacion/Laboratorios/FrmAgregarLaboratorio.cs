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

namespace SistemaVentas.Presentacion.Laboratorios
{
    public partial class FrmAgregarLaboratorio : Form
    {
        public FrmAgregarLaboratorio()
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
                        Laboratorio laboratorio = new Laboratorio();
                        laboratorio.Descripcion = txtDescripcion.Text;

                        int returnId = FLaboratorio.Insertar(laboratorio);
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
