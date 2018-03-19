using SistemaVentas.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SistemaVentas.Datos
{
    public class FDetalleCompra
    {
        public static DataSet GetAll(int iCompraId)//trae todos los clientes
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@CompraId", SqlDbType.Int,0 , iCompraId)
                };
            return DBHelper.ExecuteDataSet("usp_Datos_FDetalleCompra_GetAll", dbParams);

        }

        public static int Insertar(DetalleCompra detalleCompra,double dPorcentajeGanancia)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@CompraId", SqlDbType.Int, 0, detalleCompra.Compra.Id),
                    DBHelper.MakeParam("@ProductoId", SqlDbType.Int, 0, detalleCompra.Producto.Id),
                    DBHelper.MakeParam("@Cantidad", SqlDbType.Decimal, 0, detalleCompra.Cantidad),
                    DBHelper.MakeParam("@PrecioCosto", SqlDbType.Decimal, 0, detalleCompra.PrecioCosto),
                    DBHelper.MakeParam("@PorcentajeGanancia", SqlDbType.Decimal, 0, dPorcentajeGanancia)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FDetalleCompra_Insertar", dbParams));

        }

        public static int Actualizar(DetalleCompra detalleCompra)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, detalleCompra.Id), 
                    DBHelper.MakeParam("@CompraId", SqlDbType.Int, 0, detalleCompra.Compra.Id),
                    DBHelper.MakeParam("@ProductoId", SqlDbType.Int, 0, detalleCompra.Producto.Id),
                    DBHelper.MakeParam("@Cantidad", SqlDbType.Decimal, 0, detalleCompra.Cantidad),
                    DBHelper.MakeParam("@PrecioCosto", SqlDbType.Decimal, 0, detalleCompra.PrecioCosto)
                    
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FDetalleCompra_Actualizar", dbParams));

        }

        public static int ModificarItem(DetalleCompra detalleCompra)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, detalleCompra.Id), 
                    DBHelper.MakeParam("@Cantidad", SqlDbType.Decimal, 0, detalleCompra.Cantidad),

                    
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FDetalleCompra_ModificarItem", dbParams));

        }

        public static int Eliminar(DetalleCompra detalleCompra)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, detalleCompra.Id) 
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FDetalleCompra_Eliminar", dbParams));

        }
    }
}
