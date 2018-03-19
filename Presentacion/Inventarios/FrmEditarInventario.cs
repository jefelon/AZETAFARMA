﻿using SistemaVentas.Datos;
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
    public partial class FrmEditarInventario : Form
    {
        public FrmEditarInventario()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Id, Fecha, UsuarioId, Descripcion
                Inventario  inventario = new Inventario();
                inventario.Id = Convert.ToInt32(txtId.Text);
                inventario.Fecha = dtpFecha.Value;
                inventario.Descripcion = txtDescripcion.Text;

                if (FInventario.Actualizar(inventario) > 0)
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
