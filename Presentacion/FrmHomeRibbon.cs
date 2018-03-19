using FastReport;
using SistemaVentas.Entidad;
using SistemaVentas.Presentacion.Categorias;
using SistemaVentas.Presentacion.Clientes;
using SistemaVentas.Presentacion.Compras;
using SistemaVentas.Presentacion.Inventarios;
using SistemaVentas.Presentacion.Laboratorios;
using SistemaVentas.Presentacion.Productos;
using SistemaVentas.Presentacion.Proveedores;
using SistemaVentas.Presentacion.Reportes.RptCompras;
using SistemaVentas.Presentacion.Reportes.RptProductos;
using SistemaVentas.Presentacion.Reportes.RptVentas;
using SistemaVentas.Presentacion.Usuarios;
using SistemaVentas.Presentacion.VentasAsistente;
using SisttVentas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SistemaVentas.Presentacion.Composiciones;
using SistemaVentas.Presentacion.Lotes;
using SistemaVentas.Presentacion.Presentaciones;
using SistemaVentas.Presentacion.Secciones;

namespace SistemaVentas.Presentacion
{
    public partial class FrmHomeRibbon : Form
    {
        public FrmHomeRibbon()
        {
            InitializeComponent();
        }

        private void FrmHomeRibbon_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = Usuario.Nombre;
            lblRol.Text = Usuario.Rol;
            timer1.Enabled = true;
            if (Usuario.Rol == "Vendedor")
            {
                mostrarOpcionesUsuario(false);
            }
            if (Usuario.Rol == "Administrador")
            {
                mostrarOpcionesUsuario(true);
            }
            else
            {
                mostrarOpcionesUsuario(false);
            }
        }

        private void mostrarOpcionesUsuario(bool b)
        {
            ribbonBarProveedores.Visible = b;
            ribbonBarVentas.Visible = b;
            ribbonTabItemOtros.Visible = b;
            ribbonTabReportes.Visible = b;
            ribbonTabGestionar.Visible = b;
            ribbonBarComprar.Visible = b;
            menuStripReportes.Visible = b;
        }
        private void btnVentas_Click(object sender, EventArgs e)
        {
            FrmVentasAsistente ventas = FrmVentasAsistente.InstanciaUnica();
            ventas.Show();
        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {
            FrmCategorias cat = new FrmCategorias();
            cat.Show();
        }

        private void buttonItem14_Click(object sender, EventArgs e)
        {
            FrmClientes clientes = new FrmClientes();
            clientes.Show();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            FrmProductos prod = new FrmProductos();
            prod.Show();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            FrmProveedores prov = new FrmProveedores();
            prov.Show();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            FrmUsuarios user = new FrmUsuarios();
            user.Show();
        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            FrmVentas ventas = new FrmVentas();
            ventas.Show();
        }

        private void btnVender_Click(object sender, EventArgs e)
        {
            FrmVentasAsistente vender = FrmVentasAsistente.InstanciaUnica();
            vender.Show();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem13_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void porFechasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRptVentas rptVentas = new FrmRptVentas();
            rptVentas.Show();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblhoraActual.Text = DateTime.Now.ToString();
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            FrmCompras compras = new FrmCompras();
            compras.Show();
        }

        private void btnInventarios_Click(object sender, EventArgs e)
        {
            FrmInventarios inven = new FrmInventarios();
            inven.Show();
        }

        private void productosMásCompradosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRptProductosMasComprados rep = new FrmRptProductosMasComprados();
            rep.Show();
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            FrmComprasAsistente com = FrmComprasAsistente.InstanciaUnica();
            com.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmRptProductosMasVendidos vent = new FrmRptProductosMasVendidos();
            vent.Show();
        }

        private void listadoDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Algo importante... este reporte contiene su propio script en sql , en la base de datos no hay nada... si se quiere modificar abrir con fastreport este reporte y cambiar...
                Report rptComprobantes = new Report();
                EditarAppConfig leerConfig = new EditarAppConfig();
                rptComprobantes.Load(@"Reportes/ListadoProductos.frx");
                rptComprobantes.Dictionary.Connections[0].ConnectionString = leerConfig.leerValorConfiguracion("connectionString");
                rptComprobantes.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void listadoDeProductosPorCategoríaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Algo importante... este reporte contiene su propio script en sql , en la base de datos no hay nada... si se quiere modificar abrir con fastreport este reporte y cambiar...
                Report rptComprobantes = new Report();
                EditarAppConfig leerConfig = new EditarAppConfig();
                rptComprobantes.Load(@"Reportes/ListadoProductosPorCategorias.frx");
                rptComprobantes.Dictionary.Connections[0].ConnectionString = leerConfig.leerValorConfiguracion("connectionString");
                rptComprobantes.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void productosConStock0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Algo importante... este reporte contiene su propio script en sql , en la base de datos no hay nada... si se quiere modificar abrir con fastreport este reporte y cambiar...
                Report rptComprobantes = new Report();
                EditarAppConfig leerConfig = new EditarAppConfig();
                rptComprobantes.Load(@"Reportes/ListadoProductosStock0.frx");
                rptComprobantes.Dictionary.Connections[0].ConnectionString = leerConfig.leerValorConfiguracion("connectionString");
                rptComprobantes.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnLaboratorios_Click(object sender, EventArgs e)
        {
            FrmLaboratorios lab = new FrmLaboratorios();
            lab.Show();
        }

        private void btnComposicion_Click(object sender, EventArgs e)
        {
            FrmComposiciones form = new FrmComposiciones();
            form.Show();
        }

        private void btnLaboratorio_Click(object sender, EventArgs e)
        {
            FrmLaboratorios form = new FrmLaboratorios();
            form.Show();
        }

        private void btnLote_Click(object sender, EventArgs e)
        {
            FrmLotes form = new FrmLotes();
            form.Show();
        }

        private void btnPresentacion_Click(object sender, EventArgs e)
        {
            FrmPresentaciones form = new FrmPresentaciones();
            form.Show();
        }

        private void btnSeccion_Click(object sender, EventArgs e)
        {
            FrmSecciones form = new FrmSecciones();
            form.Show();
        }

        private void FrmVender_Click(object sender, EventArgs e)
        {

        }

    }
}
