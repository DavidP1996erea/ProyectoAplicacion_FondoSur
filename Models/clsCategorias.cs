using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class clsCategorias 
    {

        #region Atributos
        private string nombre;
        private string imagenMostrada;
        private string imagenAcertada;
        private string imagenSinAcertar;
        private bool estaAcertada;
        #endregion

        #region Propiedades
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string ImagenMostrada { get { return imagenMostrada; } set { imagenMostrada = value; } }
        public string ImagenAcertada { get { return imagenAcertada; } set { imagenAcertada = value; } }
        public string ImagenSinAcertar { get { return imagenSinAcertar; } set { imagenSinAcertar = value; } }
        public bool EstaAcertada { get { return estaAcertada; } set { estaAcertada = value; } }
        #endregion

        #region Constructores
        public clsCategorias()
        {

        }
        public clsCategorias(string nombre, string imagenMostrada, string imagenAcertada, string imagenSinAcertar ,bool estaAcertada)
        {
            Nombre = nombre;
            ImagenMostrada = imagenMostrada;
            ImagenAcertada = imagenAcertada;
            ImagenSinAcertar = imagenSinAcertar;
            EstaAcertada = estaAcertada;
        }
        #endregion
    }
}
