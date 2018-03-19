using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SistemaVentas.Entidad;

namespace SistemaVentas.Datos
{
    public class FProductoLote
    {
        public static DataSet Get(int iId)//trae todos los datos
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                     DBHelper.MakeParam("@Id", SqlDbType.Int, 0, iId),
                };
            return DBHelper.ExecuteDataSet("usp_Datos_FProductoLote_Get", dbParams);//procedimiento de la base de daatos que trae todos los registros

        }

        public static DataSet GetAll()//trae todos los datos
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                };
            return DBHelper.ExecuteDataSet("usp_Datos_FProductoLote_GetAll", dbParams);//procedimiento de la base de daatos que trae todos los registros

        }

        public static int Insertar(ProductoLote productoLote)//recibimos los parámetros como objeto de la capa entidad
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                     DBHelper.MakeParam("@ProductoId", SqlDbType.Int, 0, productoLote.Producto.Id),
                     DBHelper.MakeParam("@LoteId", SqlDbType.Int, 0, productoLote.Lote.Id)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FProductoLote_Insertar", dbParams));//procedimiento de la base de daatos que inserta datos como escalr y no como data set los registros
            

        }

        public static int Actualizar(ProductoLote productoLote)//recibimos los parámetros como objeto de la capa entidad
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, productoLote.Id),
                    DBHelper.MakeParam("@ProductoId", SqlDbType.Int, 0, productoLote.Producto.Id),
                    DBHelper.MakeParam("@LoteId", SqlDbType.Int, 0, productoLote.Lote.Id)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FProductoLote_Actualizar", dbParams));//procedimiento de la base de daatos que actualiza datos como escalr y no como data set los registros

        }

        public static int Eliminar(ProductoLote productoLote)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, productoLote.Id), // es el id que uaremos para eliminar
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FProductoLote_Eliminar", dbParams));//procedimiento de la base de daatos que elimina datos como escalar y no como data set los registros

        } 
    }
}
