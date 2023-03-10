using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class clsListadoSalas
    {

        #region Atributos

        private static List<string> listadoSalas = new List<string>();

        #endregion


        #region Propiedades

        public static List<string> ListadoSalas
        {
            get { return listadoSalas; }
            set { listadoSalas = value; }
        }
        #endregion


        /// <summary>
        /// Metodo que recibe el nombre de una sala y borra del listado de salas
        /// la sala cuando el nombre sea el mismo.
        /// </summary>
        /// <param name="nombreSala"></param>
        public static void eliminarSala(string nombreSala)
        {

            for(int i = 0; i < listadoSalas.Count; i++)
            {
                if(listadoSalas[i] == nombreSala)
                {
                    listadoSalas.RemoveAt(i);
                }
            }

        }

    }
}
