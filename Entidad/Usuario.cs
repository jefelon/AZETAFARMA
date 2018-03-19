using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaVentas.Entidad
{
    public  class Usuario
    {
        private static int _id;
        private static string _nombreUsuario;
        private static string _contrasena;
        private static string _nombre;
        private static string _apellidoPaterno;
        private static string _apellidoMaterno;
        private static string _email;
        private static string _telefono;
        private static string _celular;
        private static string _direccion;
        private static string _dni;
        private static string _rol;


        public static int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public static string NombreUsuario
        {
            get { return _nombreUsuario; }
            set { _nombreUsuario = value; }
        }

        public static string Contrasena
        {
            get { return _contrasena; }
            set { _contrasena = value; }
        }

        public static string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public static string ApellidoPaterno
        {
            get { return _apellidoPaterno; }
            set { _apellidoPaterno = value; }
        }

        public static string ApellidoMaterno
        {
            get { return _apellidoMaterno; }
            set { _apellidoMaterno = value; }
        }

        public static string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public static string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        public static string Celular
        {
            get { return _celular; }
            set { _celular = value; }
        }

        public static string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public static string Dni
        {
            get { return _dni; }
            set { _dni = value; }
        }

        public static string Rol
        {
            get { return _rol; }
            set { _rol = value; }
        }
    }
}