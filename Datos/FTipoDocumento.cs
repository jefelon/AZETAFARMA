using SistemaVentas.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SistemaVentas.Datos
{
    public class FTipoDocumento
    {
        public static DataSet GetAll()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                };
            return DBHelper.ExecuteDataSet("usp_Datos_FTipoDocumento_GetAll", dbParams);

        }

        public static int Insertar(TipoDocumento tipoDocumento)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 0, tipoDocumento.Nombre),
                     DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 0, tipoDocumento.Descripcion)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FTipoDocumento_Insertar", dbParams));


        }

        public static int Actualizar(TipoDocumento tipoDocumento)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, tipoDocumento.Id),
                    DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 0, tipoDocumento.Nombre),
                    DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 0, tipoDocumento.Descripcion)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FTipoDocumento_Actualizar", dbParams));

        }

        public static int Eliminar(TipoDocumento tipoDocumento)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, tipoDocumento.Id), 
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FTipoDocumento_Eliminar", dbParams));

        } 
    }
}
