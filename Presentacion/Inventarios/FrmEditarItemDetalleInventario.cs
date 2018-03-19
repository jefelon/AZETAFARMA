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
    public partial class FrmEditarItemDetalleInventario : Form
    {
        public FrmEditarItemDetalleInventario()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DetalleInventario edit = new DetalleInventario();
            edit.Id = Convert.ToInt32(lblDetalleInventarioId.Text);
            edit.CantidadContada = Convert.ToDouble(txtCantidadContada.Text);

            int iActualizar = FDetalleInventario.ActualizarItem(edit);//si se actualizo corectamente sumamos uno a la cantidad contada, es por que es lector de código, en cada pasada cuenta uno
            if (iActualizar > 0)
            {
                Close();
            }
        }
    }
}
