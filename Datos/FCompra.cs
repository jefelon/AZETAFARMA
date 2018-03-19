using SistemaVentas.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SistemaVentas.Datos
{
    public class FCompra
    {
        public static DataSet GetAll()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                };
            return DBHelper.ExecuteDataSet("usp_Datos_FCompra_GetAll", dbParams);

        }
        public static DataSet Get(int iCompraId)//trae todos los clientes
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@CompraId", SqlDbType.Int,0 , iCompraId)
                };
            return DBHelper.ExecuteDataSet("usp_Datos_FCompra_Get", dbParams);

        }
        public static int Insertar(Compra compra, int iUsuarioId)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@ProveedorId", SqlDbType.Int, 0, compra.Proveedor.Id), 
                    DBHelper.MakeParam("@UsuarioId", SqlDbType.Int, 0, iUsuarioId),
                    DBHelper.MakeParam("@TipoDocumentoId", SqlDbType.Int, 0, compra.TipoDocumento.Id),
                    DBHelper.MakeParam("@Fecha", SqlDbType.DateTime, 0, compra.Fecha),
                    DBHelper.MakeParam("@NumeroDocumento", SqlDbType.NChar, 0, compra.NumeroDocumento)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FCompra_Insertar", dbParams));

        }

        public static int Actualizar(Compra compra, int iUsuarioId)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {   DBHelper.MakeParam("@Id", SqlDbType.Int, 0, compra.Id),             
                    DBHelper.MakeParam("@ProveedorId", SqlDbType.Int, 0, compra.Proveedor.Id), 
                    DBHelper.MakeParam("@UsuarioId", SqlDbType.Int, 0, iUsuarioId),
                    DBHelper.MakeParam("@TipoDocumentoId", SqlDbType.Int, 0, compra.TipoDocumento.Id),
                    DBHelper.MakeParam("@Fecha", SqlDbType.DateTime, 0, compra.Fecha),
                    DBHelper.MakeParam("@NumeroDocumento", SqlDbType.NChar, 0, compra.NumeroDocumento)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FCompra_Actualizar", dbParams));

        }

        public static int Eliminar(Compra compra)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.VarChar, 0, compra.Id),
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FCompra_Eliminar", dbParams));

        }
    }
}
