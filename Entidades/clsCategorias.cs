using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class clsCategorias
    {

        #region Atributos
        private string nombre;
        private string imagen;
        private bool estaAcertada;
        #endregion


        #region Propiedades
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Imagen { get { return imagen; } set { imagen = value; } }
        public bool EstaAcertada { get { return estaAcertada; } set { estaAcertada = value; } }
        #endregion

        #region Constructores
        public clsCategorias()
        {

        }
        public clsCategorias(string nombre, string imagen, bool estaAcertada)
        {
            Nombre = nombre;
            Imagen = imagen;
            EstaAcertada = estaAcertada;
        }
        #endregion
    }
}
