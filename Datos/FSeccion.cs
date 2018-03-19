using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SistemaVentas.Entidad;

namespace SistemaVentas.Datos
{
    public class FSeccion
    {
        public static DataSet GetAll()//trae todos los datos
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                };
            return DBHelper.ExecuteDataSet("usp_Datos_FSeccion_GetAll", dbParams);//procedimiento de la base de daatos que trae todos los registros

        }

        public static int Insertar(Seccion seccion)//recibimos los parámetros como objeto de la capa entidad
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                     DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 0, seccion.Descripcion)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FSeccion_Insertar", dbParams));//procedimiento de la base de daatos que inserta datos como escalr y no como data set los registros
            

        }

        public static int Actualizar(Seccion seccion)//recibimos los parámetros como objeto de la capa entidad
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, seccion.Id),
                    DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 0, seccion.Descripcion)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FSeccion_Actualizar", dbParams));//procedimiento de la base de daatos que actualiza datos como escalr y no como data set los registros

        }

        public static int Eliminar(Seccion seccion)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, seccion.Id), // es el id que uaremos para eliminar
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FSeccion_Eliminar", dbParams));//procedimiento de la base de daatos que elimina datos como escalar y no como data set los registros

        } 
    }
}
