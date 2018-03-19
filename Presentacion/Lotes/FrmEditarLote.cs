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

namespace SistemaVentas.Presentacion.Lotes
{
    public partial class FrmEditarLote : Form
    {
        public FrmEditarLote()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Lote lote = new Lote();
                lote.Id = Convert.ToInt32(txtId.Text);
                lote.Descripcion = txtDescripcion.Text;

                if (FLote.Actualizar(lote) > 0)
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
