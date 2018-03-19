using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaVentas.Entidad
{
    public class ProductoLote
    {
        private int _id;
        private Producto _producto;
        private Lote _lote;

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


        public Lote Lote
        {
            get { return _lote; }
            set { _lote = value; }
        }

        public ProductoLote()
        {
            _producto = new Producto();
            _lote = new Lote();
        }
    }
}
