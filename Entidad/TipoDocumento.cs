using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaVentas.Entidad
{
    public class TipoDocumento
    {
        private int _id;
        private string _nombre;
        private string _descripcion;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
    }
}
