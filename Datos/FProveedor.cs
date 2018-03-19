using SistemaVentas.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SistemaVentas.Datos
{
    public class FProveedor
    {
        public static DataSet GetAll()//trae todos los datos
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                };
            return DBHelper.ExecuteDataSet("usp_Datos_FProveedor_GetAll", dbParams);//procedimiento de la base de daatos que trae todos los registros

        }

        public static int Insertar(Proveedor proveedor)//recibimos los parámetros como objeto de la capa entidad
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {                
                    DBHelper.MakeParam("@Nombre", SqlDbType.NChar, 0, proveedor.Nombre), // @Nombre asi sellama la columna de la db es de donde obtenemos el nombre
                    DBHelper.MakeParam("@Ruc", SqlDbType.NChar, 0, proveedor.Ruc),
                    DBHelper.MakeParam("@Direccion", SqlDbType.VarChar, 0, proveedor.Direccion),
                    DBHelper.MakeParam("@Telefono", SqlDbType.NChar, 0, proveedor.Telefono),
                    DBHelper.MakeParam("@Email", SqlDbType.NChar, 0, proveedor.Email),
                    DBHelper.MakeParam("@FechaRegistro", SqlDbType.DateTime, 0, proveedor.FechaRegistro)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FProveedor_Insertar", dbParams));//procedimiento de la base de daatos que inserta datos como escalr y no como data set los registros

        }

        public static int Actualizar(Proveedor proveedor)//recibimos los parámetros como objeto de la capa entidad
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, proveedor.Id),
                    DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 0, proveedor.Nombre),
                    DBHelper.MakeParam("@Ruc", SqlDbType.NChar, 0, proveedor.Ruc),
                    DBHelper.MakeParam("@Direccion", SqlDbType.VarChar, 0, proveedor.Direccion),
                    DBHelper.MakeParam("@Telefono", SqlDbType.NChar, 0, proveedor.Telefono),
                    DBHelper.MakeParam("@Email", SqlDbType.NChar, 0, proveedor.Email),
                    DBHelper.MakeParam("@FechaRegistro", SqlDbType.DateTime, 0, proveedor.FechaRegistro)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FProveedor_Actualizar", dbParams));//procedimiento de la base de daatos que actualiza datos como escalr y no como data set los registros

        }

        public static int Eliminar(Proveedor proveedor)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, proveedor.Id), // es el id que uaremos para eliminar
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FProveedor_Eliminar", dbParams));//procedimiento de la base de daatos que elimina datos como escalar y no como data set los registros

        }
    }
}
