using SistemaVentas.Presentacion;
using SistemaVentas.Presentacion.Categorias;
using SistemaVentas.Presentacion.Clientes;
using SistemaVentas.Presentacion.Productos;
using SistemaVentas.Presentacion.Proveedores;
using SistemaVentas.Presentacion.VentasAsistente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FrmLogin frm = new FrmLogin();
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
                Application.Run(new FrmHomeRibbon());

           // Application.Run(new FrmCategorias());
            //Application.Run(new FrmClientes());
            //Application.Run(new FrmProveedores());
            //Application.Run(new FrmProductos());
            //Application.Run(new FrmRoles());
           // Application.Run(new FrmPrincipal());
            //Application.Run(new FrmHomeRibbon());
            //Application.Run(FrmVentasAsistente.InstanciaUnica());
        }
    }
}