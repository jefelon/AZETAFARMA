using SistemaVentas.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SistemaVentas.Datos
{
    public class FVenta
    {
        public static DataSet GetAll()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                };
            return DBHelper.ExecuteDataSet("usp_Datos_FVenta_GetAll", dbParams);

        }
        public static DataSet Get(int iVentaId)//trae todos los clientes
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@VentaId", SqlDbType.Int,0 , iVentaId)
                };
            return DBHelper.ExecuteDataSet("usp_Datos_FVenta_Get", dbParams);

        }
        public static int Insertar(Venta venta, int iUsuarioId)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@ClienteId", SqlDbType.Int, 0, venta.Cliente.Id), 
                    DBHelper.MakeParam("@UsuarioId", SqlDbType.Int, 0, iUsuarioId),
                    DBHelper.MakeParam("@TipoDocumentoId", SqlDbType.Int, 0, venta.TipoDocumento.Id),
                    DBHelper.MakeParam("@Fecha", SqlDbType.DateTime, 0, venta.Fecha),
                    DBHelper.MakeParam("@NumeroDocumento", SqlDbType.NChar, 0, venta.NumeroDocumento)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FVenta_Insertar", dbParams));

        }

        public static int Actualizar(Venta venta, int iUsuarioId)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {   DBHelper.MakeParam("@Id", SqlDbType.Int, 0, venta.Id),             
                    DBHelper.MakeParam("@ClienteId", SqlDbType.Int, 0, venta.Cliente.Id), 
                    DBHelper.MakeParam("@UsuarioId", SqlDbType.Int, 0, iUsuarioId),
                    DBHelper.MakeParam("@TipoDocumentoId", SqlDbType.Int, 0, venta.TipoDocumento.Id),
                    DBHelper.MakeParam("@Fecha", SqlDbType.DateTime, 0, venta.Fecha),
                    DBHelper.MakeParam("@NumeroDocumento", SqlDbType.NChar, 0, venta.NumeroDocumento)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FVenta_Actualizar", dbParams));

        }

        public static int Eliminar(Venta venta)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.VarChar, 0, venta.Id),
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FVenta_Eliminar", dbParams));

        }


        public static int GenerarVenta(Venta venta, int iUsuarioId)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Fecha", SqlDbType.DateTime, 0, venta.Fecha),
                    DBHelper.MakeParam("@UsuarioId", SqlDbType.Int, 0, iUsuarioId)
                };
            return Convert.ToInt32(DBHelper.ExecuteNonQueryOutput("usp_Datos_FVenta_GenerarVenta", dbParams, "@RetornaId", SqlDbType.Int, 0));

        }
    }
}
