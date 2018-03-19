using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaVentas.Entidad
{
    public class Inventario
    {
        //Id, Fecha, UsuarioId, Descripcion
        private int _id;
        private int _usuarioId; //aqui no nos referimos a la clase Usuario pq es una clase estáatica y no puede ser modificado en tiempo de ejecucion
        private DateTime _fecha;
        private string _descripcion;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
       
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public int UsuarioId
        {
            get { return _usuarioId; }
            set { _usuarioId = value; }
        }
  

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
    }
}
