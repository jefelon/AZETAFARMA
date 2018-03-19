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

namespace SistemaVentas.Presentacion.Usuarios
{
    public partial class FrmUsuarios : Form
    {
        public FrmUsuarios()
        {
            InitializeComponent();
        }

        static DataTable dt = new DataTable();
        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            try
            {
                if (Usuario.Rol == "Administrador")
                {
                    DataSet ds = FUsuario.GetAll();
                    dt = ds.Tables[0];
                    dgvDatos.DataSource = dt;

                    if (dt.Rows.Count > 0)
                    {
                        lblSinDatos.Visible = false;
                        
                    }

                    //txtTipo.Items.Add(Usuario.Tipo);
                    //txtTipo.Items.Add("Ventas");
                    //txtTipo.Items.Add("Letras");
                    //txtTipo.Items.Add("Almacen");
                    //txtTipo.SelectedIndex = 0;
                }
                else if (Usuario.Rol == "Vendedor")
                {

                    DataSet ds = FUsuario.Get(Usuario.Id);
                    dt = ds.Tables[0];
                    dgvDatos.DataSource = dt;
                    btnAgregar.Visible = false;

                    if (dt.Rows.Count > 0)
                    {
                        lblSinDatos.Visible = false;

                    }
                }
                else
                {
                    MessageBox.Show("No tiene permiso para estar aquí.", "INTRUSO");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAgregarUsuario addUser = new FrmAgregarUsuario();
            DialogResult res = addUser.ShowDialog();

            FrmUsuario_Load(null, null);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                FrmEditarUsuario addUser = new FrmEditarUsuario();
                addUser.txtId.Text = dgvDatos.CurrentRow.Cells["Id"].Value.ToString();
                addUser.txtUsuario.Text = dgvDatos.CurrentRow.Cells["Usuario"].Value.ToString();
                addUser.txtNombre.Text = dgvDatos.CurrentRow.Cells["Nombre"].Value.ToString();
                addUser.txtApellidoPaterno.Text = dgvDatos.CurrentRow.Cells["ApellidoPaterno"].Value.ToString();
                addUser.txtApellidoMaterno.Text = dgvDatos.CurrentRow.Cells["ApellidoMaterno"].Value.ToString();
                addUser.txtEmail.Text = dgvDatos.CurrentRow.Cells["Email"].Value.ToString();
                addUser.txtDni.Text = dgvDatos.CurrentRow.Cells["Dni"].Value.ToString();
                addUser.txtTelefono.Text = dgvDatos.CurrentRow.Cells["Telefono"].Value.ToString();
                addUser.txtCelular.Text = dgvDatos.CurrentRow.Cells["Celular"].Value.ToString();
                addUser.txtDireccion.Text = dgvDatos.CurrentRow.Cells["Direccion"].Value.ToString();
                addUser.txtRol.Text = dgvDatos.CurrentRow.Cells["Rol"].Value.ToString();
                DialogResult res = addUser.ShowDialog();

                FrmUsuario_Load(null, null);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Está seguro de eliminar el elementos seleccionado?", "Eliminar Item",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (FUsuario.Eliminar(Convert.ToInt32(dgvDatos.CurrentRow.Cells["Id"].Value.ToString()))>0)
                    {
                        FrmUsuario_Load(null, null);
                    }
                }

                else
                {
                    MessageBox.Show("No se pudo Eliminar", "Eliminando...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.StackTrace);
                MessageBox.Show("No se pudo eliminar, es más probable que tenga ventas asociadas a este cliente \nPrimero deberá eliminar todas las ventas a este cliente", "No se puede eliminar");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            FrmUsuario_Load(null,null);
        }
    }
}
