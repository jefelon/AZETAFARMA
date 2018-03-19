using SistemaVentas.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SistemaVentas.Datos
{
    public class FCliente
    {
        public static DataSet GetAll()//trae todos los datos
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                };
            return DBHelper.ExecuteDataSet("usp_Datos_FCliente_GetAll", dbParams);//procedimiento de la base de daatos que trae todos los registros

        }

        public static DataSet Get( int iClienteId)//trae todos los datos
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Ruc", SqlDbType.Int, 0, iClienteId)
                };
            return DBHelper.ExecuteDataSet("usp_Datos_FCliente_Get", dbParams);//procedimiento de la base de daatos que trae todos los registros

        }

        public static int Insertar(Cliente cliente)//recibimos los parámetros como objeto de la capa entidad
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {                   
                    DBHelper.MakeParam("@Ruc", SqlDbType.NChar, 0, cliente.Ruc), // @Nombre asi sellama la columna de la db es de donde obtenemos el nombre
                    DBHelper.MakeParam("@Dni", SqlDbType.NChar, 0, cliente.Dni),
                    DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 0, cliente.Nombre),
                    DBHelper.MakeParam("@ApellidoPaterno", SqlDbType.VarChar, 0, cliente.ApellidoPaterno),
                    DBHelper.MakeParam("@ApellidoMaterno", SqlDbType.VarChar, 0, cliente.ApellidoMaterno),
                    DBHelper.MakeParam("@Email", SqlDbType.NChar, 0, cliente.Email),
                    DBHelper.MakeParam("@Telefono", SqlDbType.NChar, 0, cliente.Telefono),
                    DBHelper.MakeParam("@Celular", SqlDbType.NChar, 0, cliente.Celular),
                    DBHelper.MakeParam("@Direccion", SqlDbType.VarChar, 0, cliente.Direccion),
                    DBHelper.MakeParam("@FechaRegistro", SqlDbType.DateTime, 0, cliente.FechaRegistro)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FCliente_Insertar", dbParams));//procedimiento de la base de daatos que inserta datos como escalr y no como data set los registros

        }

        public static int Actualizar(Cliente cliente)//recibimos los parámetros como objeto de la capa entidad
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    //  , , , , , , , , , 
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, cliente.Id),
                    DBHelper.MakeParam("@Ruc", SqlDbType.NChar, 0, cliente.Ruc),
                    DBHelper.MakeParam("@Dni", SqlDbType.NChar, 0, cliente.Dni),
                    DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 0, cliente.Nombre),
                    DBHelper.MakeParam("@ApellidoPaterno", SqlDbType.VarChar, 0, cliente.ApellidoPaterno),
                    DBHelper.MakeParam("@ApellidoMaterno", SqlDbType.VarChar, 0, cliente.ApellidoMaterno),
                    DBHelper.MakeParam("@Email", SqlDbType.NChar, 0, cliente.Email),
                    DBHelper.MakeParam("@Telefono", SqlDbType.NChar, 0, cliente.Telefono),
                    DBHelper.MakeParam("@Celular", SqlDbType.NChar, 0, cliente.Celular),
                    DBHelper.MakeParam("@Direccion", SqlDbType.VarChar, 0, cliente.Direccion),
                    DBHelper.MakeParam("@FechaRegistro", SqlDbType.DateTime, 0, cliente.FechaRegistro)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FCliente_Actualizar", dbParams));//procedimiento de la base de daatos que actualiza datos como escalr y no como data set los registros

        }

        public static int Eliminar(Cliente cliente)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, cliente.Id), // es el id que uaremos para eliminar
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FCliente_Eliminar", dbParams));//procedimiento de la base de daatos que elimina datos como escalar y no como data set los registros

        } 
    }
}
