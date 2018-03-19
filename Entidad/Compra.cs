using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SistemaVentas.Entidad;

namespace SistemaVentas.Entidad
{
   public class Compra
    {
        private int _id;
        private Proveedor _proveedor;
        private Usuario _usuario;
        private DateTime _fecha;
        private TipoDocumento _tipoDocumento;
        private string _numeroDocumento;

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
       
        public Usuario Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        public TipoDocumento TipoDocumento
        {
            get { return _tipoDocumento; }
            set { _tipoDocumento = value; }
        }
        public string NumeroDocumento
        {
            get { return _numeroDocumento; }
            set { _numeroDocumento = value; }
        }
        public Compra()
        {
            _proveedor = new Proveedor();
            _usuario = new Usuario();
            _tipoDocumento = new TipoDocumento();
        }
    }
}
