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

namespace SistemaVentas.Presentacion.Reportes.RptCompras
{
    public partial class FrmRptProductosMasComprados : Form
    {
        public FrmRptProductosMasComprados()
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
                rptComprobantes.Load(@"Reportes/ProductosMasComprados.frx");
                rptComprobantes.Dictionary.Connections[0].ConnectionString = leerConfig.leerValorConfiguracion("connectionString");
                rptComprobantes.SetParameterValue("Titulo", "COMPRAS DE  " + leerConfig.leerValorConfiguracion("NombreEmpresa") + " Desde:  " + dtpDesde.Value.Date.ToString("dd/MM/yyyy") + " Hasta: " + dtpHasta.Value.Date.ToString("dd/MM/yyyy"));
                rptComprobantes.SetParameterValue("Desde", dtpDesde.Value);
                rptComprobantes.SetParameterValue("Hasta", dtpHasta.Value);
                rptComprobantes.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
