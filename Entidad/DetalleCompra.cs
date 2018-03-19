using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SistemaVentas.Entidad;

namespace SistemaVentas.Entidad
{
    public class DetalleCompra
    {
        private int _id;
        private Compra _compra;
        private Producto _producto;
        private double _cantidad;
        private double _precioCosto;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        public Compra Compra
        {
            get { return _compra; }
            set { _compra = value; }
        }
       
        public Producto Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }

        public double Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public double PrecioCosto
        {
            get { return _precioCosto; }
            set { _precioCosto = value; }
        }

        public DetalleCompra()
        {
            _compra   = new Compra();
            _producto = new Producto();
        }
    }
}
