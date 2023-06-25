using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    
    public class Validacion
        
    {

        public string cajaFiltro { get; set; }
        public string selector { get; set; }
        
        
        public bool ValidarFiltro(int validar)
        {

            if (validar < 0 && selector=="Campo")
            {
                MessageBox.Show("Seleccione Campo");
                return true;
            }
            if (validar < 0 &&  selector == "Criterio")
            {
                MessageBox.Show("Seleccione Criterio");
                 return true;
            }

           
            return false;
        }

        public bool ValidarFiltro(string validar)
        {
            if (validar == "Id" || validar == "Precio")
            {
                if (string.IsNullOrEmpty(cajaFiltro))
                {
                    MessageBox.Show("Filtro no puede ser vacio");
                     return true;
                }
                if (!(SoloNumero(cajaFiltro)))
                {
                    MessageBox.Show("Sólo Campo Numérico");
                    return true;
                }
            }
            
            return false;
        }
       

        public bool NoVacio(string txtBox)
        {
            if (string.IsNullOrEmpty(txtBox))
            {
                MessageBox.Show("El item Precio no puede ser vacio");
                return true;
            }
            if (!(SoloNumero(txtBox)))
            {
                MessageBox.Show("El item Precio es de sólo campo numérico");
                return true;
            }
            return false;
        }
         
        public bool noVacio(string txtBox)
        {
            if (string.IsNullOrEmpty(txtBox) && cajaFiltro=="Codigo")
            {
                MessageBox.Show("El item Código no puede ser vacio");
                return true;
            }
            if (string.IsNullOrEmpty(txtBox) && cajaFiltro == "Nombre")
            {
                MessageBox.Show("El item Nombre no puede ser vacio");
                return true;
            }
            if (string.IsNullOrEmpty(txtBox) && cajaFiltro == "Descripcion")
            {
                MessageBox.Show("El item Descripción no puede ser vacio");
                return true;
            }
            if (string.IsNullOrEmpty(txtBox) && cajaFiltro == "UrlImagen")
            {
                MessageBox.Show("El item UrlImagen no puede ser vacio");
                return true;
            }

            return false;
        }

         public static bool SoloNumero(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
