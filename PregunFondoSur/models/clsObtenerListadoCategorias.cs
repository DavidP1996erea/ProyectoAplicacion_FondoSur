using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregunFondoSur.models
{
    public class clsObtenerListadoCategorias
    {
        public static List<clsCategoriasMaui> obtenerListadoCompletoCategorias()
        {
            List<clsCategoriasMaui> listaCategorias = new List<clsCategoriasMaui>();

            listaCategorias.Add(new clsCategoriasMaui("Films", "videobase.png", "videoganado.png", "videobase.png", false));
            listaCategorias.Add(new clsCategoriasMaui("Music", "musicalbase.png", "musicalganado.png", "musicalbase.png", false));
            listaCategorias.Add(new clsCategoriasMaui("History", "historybase.png", "historyganado.png", "historybase.png", false));
            listaCategorias.Add(new clsCategoriasMaui("Food", "foodbase.png", "foodganado.png", "foodbase.png", false));
            listaCategorias.Add(new clsCategoriasMaui("Science", "sciencebase.png", "scienceganado.png", "sciencebase.png", false));

            return listaCategorias;
        }
    }
}
