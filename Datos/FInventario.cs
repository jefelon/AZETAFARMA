using SistemaVentas.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SistemaVentas.Datos
{
    public class FInventario
    {
        public static DataSet GetAll()//trae todos los datos
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                };
            return DBHelper.ExecuteDataSet("usp_Datos_FInventario_GetAll", dbParams);

        }

        public static int Insertar(Inventario inventario)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Fecha", SqlDbType.DateTime, 0, inventario.Fecha), 
                    DBHelper.MakeParam("@UsuarioId", SqlDbType.Int, 0, Usuario.Id),
                    DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 0, inventario.Descripcion)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FInventario_Insertar", dbParams));

        }

        public static int Actualizar(Inventario inventario)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, inventario.Id),
                    DBHelper.MakeParam("@Fecha", SqlDbType.DateTime, 0, inventario.Fecha), 
                    DBHelper.MakeParam("@UsuarioId", SqlDbType.Int, 0, Usuario.Id),
                    DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 0, inventario.Descripcion)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FInventario_Actualizar", dbParams));
        }

        public static int Eliminar(Inventario inventario)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, inventario.Id), 
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FInventario_Eliminar", dbParams));
        } 
    }
}
