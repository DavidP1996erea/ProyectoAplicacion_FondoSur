using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregunFondoSur.models
{
    public class clsObtenerListadoCategorias
    {
        public static List<clsCategorias> obtenerListadoCompletoCategorias()
        {
            List<clsCategorias> listaCategorias = new List<clsCategorias>();

            listaCategorias.Add(new clsCategorias("Films", "URL", false));
            listaCategorias.Add(new clsCategorias("Music", "URL", false));
            listaCategorias.Add(new clsCategorias("History", "URL", false));
            listaCategorias.Add(new clsCategorias("Food", "URL", false));
            listaCategorias.Add(new clsCategorias("Science", "URL", false));

            return listaCategorias;
        }
    }
}
