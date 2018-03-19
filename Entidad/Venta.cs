using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaVentas.Entidad
{
    public class Venta
    {
        private int _id;
        private Cliente _cliente;
        private Usuario _usuario;
        private DateTime _fecha;
        private string _numeroDocumento;
        private TipoDocumento _tipoDocumento;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Cliente Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
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


        public string NumeroDocumento
        {
            get { return _numeroDocumento; }
            set { _numeroDocumento = value; }
        }

        public TipoDocumento TipoDocumento
        {
            get { return _tipoDocumento; }
            set { _tipoDocumento = value; }
        }

        public Venta()
        {
            _cliente = new Cliente();
            _usuario = new Usuario();
            _tipoDocumento = new TipoDocumento();
        }
    }
}