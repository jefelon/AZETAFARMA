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

namespace SistemaVentas.Presentacion.Composiciones
{
    public partial class FrmEditarComposicion : Form
    {
        public FrmEditarComposicion()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Composicion composicion = new Composicion();
                composicion.Id = Convert.ToInt32(txtId.Text);
                composicion.Descripcion = txtDescripcion.Text;

                if (FComposicion.Actualizar(composicion) > 0)
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
