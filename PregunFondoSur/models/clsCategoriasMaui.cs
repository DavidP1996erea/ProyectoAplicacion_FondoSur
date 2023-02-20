
using Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PregunFondoSur.models
{
    public class clsCategoriasMaui : clsCategorias, INotifyPropertyChanged
    {



        #region Atributos
        public event PropertyChangedEventHandler PropertyChanged;
       
        #endregion

        #region Propiedades
        public string ImagenMostrada
        {
            get { return base.ImagenMostrada; }
            set {base.ImagenMostrada = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Constructores
        public clsCategoriasMaui()
        {

        }
        public clsCategoriasMaui(string nombre, string imagenMostrada, string imagenAcertada, string imagenSinAcertar, bool estaAcertada) : base( nombre, imagenMostrada,  imagenAcertada,  imagenSinAcertar, estaAcertada)
        {
            this.Nombre = nombre;
            this.ImagenMostrada = imagenMostrada;
            this.ImagenAcertada = imagenAcertada;
            this.ImagenSinAcertar = imagenSinAcertar;
            this.EstaAcertada = estaAcertada;

        }
        #endregion



        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
