using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaVentas.Entidad
{
    public class Cliente
    {
        private int _id;
        private string _ruc;
        private string _dni;
        private string _nombre;
        private string _apellidoPaterno;
        private string _apellidoMaterno;
        private string _email;
        private string _telefono;
        private string _celular;
        private string _direccion;
        private DateTime _fechaRegistro;


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Ruc
        {
            get { return _ruc; }
            set { _ruc = value; }
        }

        public string Dni
        {
            get { return _dni; }
            set { _dni = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string ApellidoPaterno
        {
            get { return _apellidoPaterno; }
            set { _apellidoPaterno = value; }
        }
        public string ApellidoMaterno
        {
            get { return _apellidoMaterno; }
            set { _apellidoMaterno = value; }
        }


        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }


        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        public string Celular
        {
            get { return _celular; }
            set { _celular = value; }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public DateTime FechaRegistro
        {
            get { return _fechaRegistro; }
            set { _fechaRegistro = value; }
        }

        //constructor
        public Cliente()
        {
        }

        public Cliente(int id, string ruc, string dni, string nombre, string apellidoPaterno, string apellidoMaterno, string email, string telefono, string celular, string direccion, DateTime fechaRegistro)
        {
            _id = id;
            _ruc = ruc;
            _dni = dni;
            _nombre = nombre;
            _apellidoPaterno = apellidoMaterno;
            _apellidoMaterno = apellidoMaterno;
            _email = email;
            _telefono = telefono;
            _celular = celular;
            _direccion = direccion;
            _fechaRegistro = fechaRegistro;
        }
    }
}
