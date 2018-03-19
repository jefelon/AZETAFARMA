using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaVentas.Entidad
{
    public class ProductoComposicion
    {
        private int _id;
        private Producto _producto;
        private Composicion _composicion;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Producto Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }


        public Composicion Composicion
        {
            get { return _composicion; }
            set { _composicion = value; }
        }

        public ProductoComposicion()
        {
            _producto = new Producto();
            _composicion = new Composicion();
        }
    }
}
