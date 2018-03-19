using SistemaVentas.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SistemaVentas.Datos
{
    public class FDetalleVenta
    {
        public static DataSet GetAll(int iVentaId)//trae todos los clientes
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@VentaId", SqlDbType.Int,0 , iVentaId)
                };
            return DBHelper.ExecuteDataSet("usp_Datos_FDetalleVenta_GetAll", dbParams);

        }

        public static int Insertar(DetalleVenta detalleVenta)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@VentaId", SqlDbType.Int, 0, detalleVenta.Venta.Id),
                    DBHelper.MakeParam("@ProductoId", SqlDbType.Int, 0, detalleVenta.Producto.Id),
                    DBHelper.MakeParam("@Cantidad", SqlDbType.Decimal, 0, detalleVenta.Cantidad),
                    DBHelper.MakeParam("@PrecioUnitario", SqlDbType.Decimal, 0, detalleVenta.PrecioUnitario)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FDetalleVenta_Insertar", dbParams));

        }

        public static int Actualizar(DetalleVenta detalleVenta)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, detalleVenta.Id), 
                    DBHelper.MakeParam("@VentaId", SqlDbType.Int, 0, detalleVenta.Venta.Id),
                    DBHelper.MakeParam("@ProductoId", SqlDbType.Int, 0, detalleVenta.Producto.Id),
                    DBHelper.MakeParam("@Cantidad", SqlDbType.Decimal, 0, detalleVenta.Cantidad),
                    DBHelper.MakeParam("@PrecioUnitario", SqlDbType.Decimal, 0, detalleVenta.PrecioUnitario)
                    
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FDetalleVenta_Actualizar", dbParams));

        }

        public static int ModificarItem(DetalleVenta detalleVenta)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, detalleVenta.Id), 
                    DBHelper.MakeParam("@Cantidad", SqlDbType.Decimal, 0, detalleVenta.Cantidad),

                    
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FDetalleVenta_ModificarItem", dbParams));

        }

        public static int Eliminar(DetalleVenta detalleVenta)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, detalleVenta.Id) 
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FDetalleVenta_Eliminar", dbParams));

        }
    }
}
