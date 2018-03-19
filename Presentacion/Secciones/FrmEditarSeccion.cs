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

namespace SistemaVentas.Presentacion.Secciones
{
    public partial class FrmEditarSeccion : Form
    {
        public FrmEditarSeccion()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Seccion seccion = new Seccion();
                seccion.Id = Convert.ToInt32(txtId.Text);
                seccion.Descripcion = txtDescripcion.Text;

                if (FSeccion.Actualizar(seccion) > 0)
                {
                    MessageBox.Show(txtDescripcion.Text + " se modificó correctamente");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
