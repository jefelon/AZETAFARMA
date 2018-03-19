using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaVentas.Entidad
{
    public class Producto
    {
        //  StockMinimo, StockMaximo,PresentacionId, LaboratorioId, UbicacionId, SeccionId, Vencimiento
        private int _id;
        private Proveedor _proveedor;
        private string _codigo;
        private string _nombre;
        private double _precioCompra;
        private double _precioVenta;
        private byte[] _imagen;
        private double _stock;

        private double _stockMinimo;
        private double _stockMaximo;
        private Presentacion_ _presentacion;
        private Laboratorio _laboratorio;
        private String _ubicacion;
        private Seccion _seccion;
        private string _indicacion;
        private DateTime _fechaRegistro;
        private string _composicion;
        private DateTime _vencimiento;
        


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Proveedor Proveedor
        {
            get { return _proveedor; }
            set { _proveedor = value; }
        }


        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public double PrecioVenta
        {
            get { return _precioVenta; }
            set { _precioVenta = value; }
        }

        public double PrecioCompra
        {
            get { return _precioCompra; }
            set { _precioCompra = value; }
        }


        public byte[] Imagen
        {
            get { return _imagen; }
            set { _imagen = value; }
        }
        public double Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }

        public double StockMinimo
        {
            get { return _stockMinimo; }
            set { _stockMinimo = value; }
        }

        public double StockMaximo
        {
            get { return _stockMaximo; }
            set { _stockMaximo = value; }
        }

        public Presentacion_ Presentacion_
        {
            get { return _presentacion; }
            set { _presentacion = value; }
        }

        public Laboratorio Laboratorio
        {
            get { return _laboratorio; }
            set { _laboratorio = value; }
        }

        public string Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
        }

        public Seccion Seccion
        {
            get { return _seccion; }
            set { _seccion = value; }
        }

        public string Indicacion
        {
            get { return _indicacion; }
            set { _indicacion = value; }
        }
        public DateTime FechaRegistro
        {
            get { return _fechaRegistro; }
            set { _fechaRegistro = value; }
        }

        public string Composicion
        {
            get { return _composicion; }
            set { _composicion = value; }
        }

        public DateTime Vencimiento
        {
            get { return _vencimiento; }
            set { _vencimiento = value; }
        }

        public Producto()
        {

            _proveedor = new Proveedor();
            _presentacion = new Presentacion_();
            _laboratorio = new Laboratorio();
            _seccion = new Seccion();
        }


        public Producto(int id, Proveedor proveedor,string codigo, string nombre, string descripcion, double stock, double precioCompra,
            double precioventa, byte[] imagen,string unidad,double stockMinimo,double stockMaximo,Presentacion_ presentacion,
            Laboratorio laboratorio,string ubicacion,Seccion seccion,string indicacion,DateTime fechaRegistro,string composicion,DateTime vencimiento)
        {
            Id = id;
            Proveedor = proveedor;
            Codigo = codigo;
            Nombre = nombre;
            PrecioCompra = precioCompra;
            PrecioVenta = precioventa;
            Imagen = imagen;
            Stock = stock;
            StockMinimo = stockMinimo;
            StockMaximo = stockMaximo;
            Presentacion_ = presentacion;
            Laboratorio = laboratorio;
            Ubicacion = ubicacion;
            Seccion = seccion;
            Indicacion = indicacion;
            FechaRegistro = fechaRegistro;
            Vencimiento = vencimiento;
            Composicion = composicion;
        }
    }
}
