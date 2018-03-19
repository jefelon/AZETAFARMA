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

namespace SistemaVentas.Presentacion.Categorias
{
    public partial class FrmEditarCategoria : Form
    {
        public FrmEditarCategoria()
        {
            InitializeComponent();
        }

        private void FrmEditarCategoria_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Categoria categoria = new Categoria();
                categoria.Id = Convert.ToInt32(txtId.Text);
                categoria.Descripcion = txtDescripcion.Text;

                if (FCategoria.Actualizar(categoria) >0)
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
