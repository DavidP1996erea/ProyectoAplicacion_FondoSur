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

            listaCategorias.Add(new clsCategorias("Films", "videobase.png", "videoganado.png", "videobase.png", false));
            listaCategorias.Add(new clsCategorias("Music", "musicalbase.png", "musicalganado.png", "musicalbase.png", false));
            listaCategorias.Add(new clsCategorias("History", "historybase.png", "historyganado.png", "historybase.png", false));
            listaCategorias.Add(new clsCategorias("Food", "foodbase.png", "foodganado.png", "foodbase.png", false));
            listaCategorias.Add(new clsCategorias("Science", "sciencebase.png", "scienceganado.png", "sciencebase.png", false));

            return listaCategorias;
        }
    }
}
