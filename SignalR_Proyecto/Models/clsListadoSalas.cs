using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class clsListadoSalas
    {

        #region Atributos

        private static List<string> listadoSalas;

        #endregion


        #region Propiedades

        public static List<string> ListadoSalas
        {
            get { return listadoSalas; }
            set { listadoSalas = value; }
        }
        #endregion


        #region Constructor por defecto

        public clsListadoSalas()
        {
            listadoSalas = new List<string>();
        }

        #endregion

    }
}
