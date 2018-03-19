using SistemaVentas.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SistemaVentas.Datos
{
    public class FProducto
    {
        public static DataSet Get(int iProductoId)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, iProductoId),
                };
            return DBHelper.ExecuteDataSet("usp_Datos_FProducto_Get", dbParams);//procedimiento de la base de daatos que trae un  registro

        }
        public static DataSet GetAll()//trae todos los datos
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                };
            return DBHelper.ExecuteDataSet("usp_Datos_FProducto_GetAll", dbParams);//procedimiento de la base de daatos que trae todos los registros

        }

        public static int Insertar(Producto producto)//recibimos los parámetros como objeto de la capa entidad
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {    //  StockMinimo, StockMaximo,PresentacionId, LaboratorioId, UbicacionId, SeccionId, Vencimiento      
                    DBHelper.MakeParam("@ProveedorId", SqlDbType.Int, 0, producto.Proveedor.Id), // @Nombre asi sellama la columna de la db es de donde obtenemos el nombre
                    DBHelper.MakeParam("@Codigo", SqlDbType.NChar, 0, producto.Codigo),
                    DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 0, producto.Nombre),
                    DBHelper.MakeParam("@PrecioVenta", SqlDbType.Decimal, 0, producto.PrecioVenta),
                    DBHelper.MakeParam("@PrecioCompra", SqlDbType.Decimal, 0, producto.PrecioCompra),
                    DBHelper.MakeParam("@Stock", SqlDbType.Decimal, 0, producto.Stock),
                    DBHelper.MakeParam("@StockMinimo", SqlDbType.Decimal, 0, producto.StockMinimo),
                    DBHelper.MakeParam("@StockMaximo", SqlDbType.Decimal, 0, producto.StockMaximo),
                    DBHelper.MakeParam("@PresentacionId", SqlDbType.Int, 0, producto.Presentacion_.Id),
                    DBHelper.MakeParam("@LaboratorioId", SqlDbType.Int, 0, producto.Laboratorio.Id),
                    DBHelper.MakeParam("@Ubicacion", SqlDbType.VarChar, 0, producto.Ubicacion),
                    DBHelper.MakeParam("@SeccionId", SqlDbType.Int, 0, producto.Seccion.Id),
                    DBHelper.MakeParam("@Indicacion", SqlDbType.VarChar, 0, producto.Indicacion),
                    DBHelper.MakeParam("@FechaRegistro", SqlDbType.Date, 0, producto.FechaRegistro),
                    DBHelper.MakeParam("@Composicion", SqlDbType.VarChar, 0, producto.Composicion),
                    DBHelper.MakeParam("@Vencimiento", SqlDbType.Date, 0, producto.Vencimiento),
                    
                    //DBHelper.MakeParam("@Imagen", SqlDbType.Image, 0, producto.Imagen)
                    
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FProducto_Insertar", dbParams));//procedimiento de la base de daatos que inserta datos como escalr y no como data set los registros

        }

        public static int Actualizar(Producto producto)//recibimos los parámetros como objeto de la capa entidad
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, producto.Id),
                    DBHelper.MakeParam("@ProveedorId", SqlDbType.Int, 0, producto.Proveedor.Id), // @Nombre asi sellama la columna de la db es de donde obtenemos el nombre
                    DBHelper.MakeParam("@Codigo", SqlDbType.NChar, 0, producto.Codigo),
                    DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 0, producto.Nombre),
                    DBHelper.MakeParam("@PrecioVenta", SqlDbType.Decimal, 0, producto.PrecioVenta),
                    DBHelper.MakeParam("@PrecioCompra", SqlDbType.Decimal, 0, producto.PrecioCompra),
                    DBHelper.MakeParam("@Stock", SqlDbType.Decimal, 0, producto.Stock),
                    DBHelper.MakeParam("@StockMinimo", SqlDbType.Decimal, 0, producto.StockMinimo),
                    DBHelper.MakeParam("@StockMaximo", SqlDbType.Decimal, 0, producto.StockMaximo),
                    DBHelper.MakeParam("@PresentacionId", SqlDbType.Int, 0, producto.Presentacion_.Id),
                    DBHelper.MakeParam("@LaboratorioId", SqlDbType.Int, 0, producto.Laboratorio.Id),
                    DBHelper.MakeParam("@Ubicacion", SqlDbType.VarChar, 0, producto.Ubicacion),
                    DBHelper.MakeParam("@SeccionId", SqlDbType.Int, 0, producto.Seccion.Id),
                    DBHelper.MakeParam("@Indicacion", SqlDbType.VarChar, 0, producto.Indicacion),
                    DBHelper.MakeParam("@FechaRegistro", SqlDbType.Date, 0, producto.FechaRegistro),
                    DBHelper.MakeParam("@Composicion", SqlDbType.VarChar, 0, producto.Composicion),
                    DBHelper.MakeParam("@Vencimiento", SqlDbType.Date, 0, producto.Vencimiento),
                    //DBHelper.MakeParam("@Imagen", SqlDbType.Image, 0, producto.Imagen)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FProducto_Actualizar", dbParams));//procedimiento de la base de daatos que actualiza datos como escalr y no como data set los registros

        }

        public static int Eliminar(Producto producto)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, producto.Id), // es el id que usaremos para eliminar
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FProducto_Eliminar", dbParams));//procedimiento de la base de daatos que elimina datos como escalar y no como data set los registros

        }

        public static DataSet Buscar(string nombre)//trae todos los datos
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 0, nombre)
                };
            return DBHelper.ExecuteDataSet("usp_Datos_FProducto_Buscar", dbParams);//procedimiento de la base de daatos que trae todos los registros

        }

        public static DataSet BuscarMismaComposicion(string criterio)//trae todos los datos
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Criterio", SqlDbType.VarChar, 0, criterio)
                };
            return DBHelper.ExecuteDataSet("usp_Datos_FProducto_BuscarMismaComposicion", dbParams);//procedimiento de la base de daatos que trae todos los registros

        }

        public static DataSet BuscarDistintoLaboratorio(string criterio, string laboratorio)//trae todos los datos
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Criterio", SqlDbType.VarChar, 0, criterio),
                    DBHelper.MakeParam("@Laboratorio", SqlDbType.VarChar, 0, laboratorio)
                };
            return DBHelper.ExecuteDataSet("usp_Datos_FProducto_BuscarDistintoLaboratorio", dbParams);//procedimiento de la base de daatos que trae todos los registros

        }
        public static DataSet BuscarPorIndicacion(string indicacion)//trae todos los datos
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Indicacion", SqlDbType.VarChar, 0, indicacion),
                };
            return DBHelper.ExecuteDataSet("usp_Datos_FProducto_BuscarPorIndicacion", dbParams);//procedimiento de la base de daatos que trae todos los registros

        }
        
    }
}
