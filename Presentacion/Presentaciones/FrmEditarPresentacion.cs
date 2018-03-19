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
    public partial class FrmEditarPresentacion : Form
    {
        public FrmEditarPresentacion()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Presentacion_ presentacion = new Presentacion_();
                presentacion.Id = Convert.ToInt32(txtId.Text);
                presentacion.Descripcion = txtDescripcion.Text;

                if (FPresentacion_.Actualizar(presentacion) > 0)
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
