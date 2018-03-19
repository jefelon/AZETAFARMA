using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaVentas.Entidad
{
    public class ProductoCategoria
    {
        private int _id;
        private Producto _producto;
        private Categoria _categoria;

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


        public Categoria Categoria
        {
            get { return _categoria; }
            set { _categoria = value; }
        }

        public ProductoCategoria()
        {
            _producto = new Producto();
            _categoria = new Categoria();
        }
    }
}
