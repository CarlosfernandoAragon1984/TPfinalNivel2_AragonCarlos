﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
   public class Marca
    {
        //propiedades de Marca
        public int id { get; set; }
        public string descripcion { get; set; }


        // sobre escribimos el método ToString() para que devuelva la descripcion
        public override string ToString()
        {
            return descripcion;
        }
    }
}
