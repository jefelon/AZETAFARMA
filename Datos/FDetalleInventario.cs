using SistemaVentas.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SistemaVentas.Datos
{
    public class FDetalleInventario
    {
        public static DataSet GetAll()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                };
            return DBHelper.ExecuteDataSet("usp_Datos_FInventario_GetAll", dbParams);

        }

        public static DataSet Get(int iInventarioId)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                     DBHelper.MakeParam("@InventarioId", SqlDbType.Int, 0, iInventarioId)
                };
            return DBHelper.ExecuteDataSet("usp_Datos_FDetalleInventario_Get", dbParams);

        }
        public static DataSet BuscarDetalleInventario(string sCodigoProducto,int inventarioId)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                     DBHelper.MakeParam("@Codigo", SqlDbType.VarChar, 0, sCodigoProducto),
                     DBHelper.MakeParam("@InventarioId", SqlDbType.Int, 0, inventarioId)
                };
            return DBHelper.ExecuteDataSet("usp_Datos_FDetalleInventario_Buscar", dbParams);

        }
        public static int Insertar(DetalleInventario detalleInventario)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    //Id, InventarioId, ProductoId, CantidadContada, CantidadSistema
                    DBHelper.MakeParam("@InventarioId", SqlDbType.Int, 0, detalleInventario.Inventario.Id), 
                    DBHelper.MakeParam("@Codigo", SqlDbType.VarChar, 0, detalleInventario.Producto.Codigo),
                    DBHelper.MakeParam("@CantidadContada", SqlDbType.Decimal, 0, detalleInventario.CantidadContada),
                    //DBHelper.MakeParam("@CantidadSistema", SqlDbType.Decimal, 0, detalleInventario.CantidadSistema)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FDetalleInventario_Insertar", dbParams));

        }

        public static int Actualizar(DetalleInventario detalleInventario)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, detalleInventario.Id),
                    DBHelper.MakeParam("@CantidadContada", SqlDbType.Decimal, 0, detalleInventario.CantidadContada),
                    DBHelper.MakeParam("@ProductoId", SqlDbType.Int, 0, detalleInventario.Producto.Id)
                    //DBHelper.MakeParam("@CantidadSistema", SqlDbType.Decimal, 0, detalleInventario.CantidadSistema)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FDetalleInventario_Actualizar", dbParams));
        }
        public static int ActualizarItem(DetalleInventario detalleInventario)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, detalleInventario.Id),
                    DBHelper.MakeParam("@CantidadContada", SqlDbType.Decimal, 0, detalleInventario.CantidadContada),
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FDetalleInventario_ActualizarItem", dbParams));
        }

        public static int Eliminar(DetalleInventario detalleInventario)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, detalleInventario.Id), 
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FDetalleInventario_Eliminar", dbParams));
        } 
    }
}
