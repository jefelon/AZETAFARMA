﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SistemaVentas.Entidad;

namespace SistemaVentas.Datos
{
    public class FPresentacion_
    {
        public static DataSet GetAll()//trae todos los datos
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                };
            return DBHelper.ExecuteDataSet("usp_Datos_FPresentacion_GetAll", dbParams);//procedimiento de la base de daatos que trae todos los registros

        }

        public static int Insertar(Presentacion_ presentacion)//recibimos los parámetros como objeto de la capa entidad
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                     DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 0, presentacion.Descripcion)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FPresentacion_Insertar", dbParams));//procedimiento de la base de daatos que inserta datos como escalr y no como data set los registros
            

        }

        public static int Actualizar(Presentacion_ presentacion)//recibimos los parámetros como objeto de la capa entidad
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, presentacion.Id),
                    DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 0, presentacion.Descripcion)
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FPresentacion_Actualizar", dbParams));//procedimiento de la base de daatos que actualiza datos como escalr y no como data set los registros

        }

        public static int Eliminar(Presentacion_ presentacion)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@Id", SqlDbType.Int, 0, presentacion.Id), // es el id que uaremos para eliminar
                };
            return Convert.ToInt32(DBHelper.ExecuteScalar("usp_Datos_FPresentacion_Eliminar", dbParams));//procedimiento de la base de daatos que elimina datos como escalar y no como data set los registros

        } 
    }
}
