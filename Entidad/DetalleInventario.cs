using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SistemaVentas.Entidad;

namespace SistemaVentas.Entidad
{
   public class DetalleInventario
    {
        //Id, InventarioId, ProductoId, CantidadContada, CantidadSistema
        private int _id;
        private Inventario _inventario;
        private Producto _producto;
        private double _cantidadContada;
        private double _cantidadSistema;//esta propiedad en realidad lo manejará el sistema...

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Inventario Inventario
        {
            get { return _inventario; }
            set { _inventario = value; }
        }

        public Producto Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }

        public double CantidadContada
        {
            get { return _cantidadContada; }
            set { _cantidadContada = value; }
        }

        public double CantidadSistema
        {
            get { return _cantidadSistema; }
            set { _cantidadSistema = value; }
        }

        public DetalleInventario()
        {
            _inventario = new Inventario();
            _producto    = new Producto();
        }
    }
}
