using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SistemaVentas.Entidad;

namespace SistemaVentas.Datos
{
    public class FProductoCategoria
    {
        public static DataSet Get(int iId)//trae todos los datos
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                     DBHelper.MakeParam("@Id", SqlDbType.Int, 0, iId),
                };
            return DBHelper.ExecuteDataSet("usp_Datos_FProductoCategoria_Get", dbParams);//procedimiento de la base de daatos que trae todos los registros

        }

        public static DataSet GetAll()//trae todos los datos
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                };
            return DBHelper.ExecuteDataSet("usp_Datos_FProductoCategoria_GetAll", dbParams);//procedimiento de la base de daatos que trae todos los registros

        }

        public static int Insertar(ProductoCategoria productoCategoria)//recibimos los parámetros como objeto de la capa entidad
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                     DBHelper.MakeParam("@ProductoId", SqlDbType.Int, 0, productoCategoria.Producto.Id),
                     DBHelper.MakeParam("@CategoriaId", SqlDbType.Int, 0, productoCategoria.Categoria.Id)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FProductoCategoria_Insertar", dbParams));//procedimiento de la base de daatos que inserta datos como escalr y no como data set los registros
            

        }

        public static int Actualizar(ProductoCategoria productoCategoria)//recibimos los parámetros como objeto de la capa entidad
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, productoCategoria.Id),
                    DBHelper.MakeParam("@ProductoId", SqlDbType.Int, 0, productoCategoria.Producto.Id),
                    DBHelper.MakeParam("@CategoriaId", SqlDbType.Int, 0, productoCategoria.Categoria.Id)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FProductoCategoria_Actualizar", dbParams));//procedimiento de la base de daatos que actualiza datos como escalr y no como data set los registros

        }

        public static int Eliminar(ProductoCategoria productoCategoria)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, productoCategoria.Id), // es el id que uaremos para eliminar
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FProductoCategoria_Eliminar", dbParams));//procedimiento de la base de daatos que elimina datos como escalar y no como data set los registros

        } 
    }
}
