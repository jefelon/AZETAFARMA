﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaVentas.Entidad
{
    public class Laboratorio
    {
        private int _id;
        private string _descripcion;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
    }
}
