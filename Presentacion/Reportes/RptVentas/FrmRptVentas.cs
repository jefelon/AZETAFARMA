using FastReport;
using SisttVentas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SistemaVentas.Presentacion.Reportes.RptVentas
{
    public partial class FrmRptVentas : Form
    {
        public FrmRptVentas()
        {
            InitializeComponent();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                //Algo importante... este reporte contiene su propio script en sql , en la base de datos no hay nada... si se quiere modificar abrir con fastreport este reporte y cambiar...
                Report rptComprobantes = new Report();
                EditarAppConfig leerConfig = new EditarAppConfig();
                rptComprobantes.Load(@"Reportes/RptVentas.frx");
                rptComprobantes.SetParameterValue("titulo", "VENTAS DE  " + leerConfig.leerValorConfiguracion("NombreEmpresa"));
                rptComprobantes.SetParameterValue("Desde", dtpDesde.Value);
                rptComprobantes.SetParameterValue("Hasta", dtpHasta.Value);
                rptComprobantes.Dictionary.Connections[0].ConnectionString = leerConfig.leerValorConfiguracion("connectionString");
                rptComprobantes.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
