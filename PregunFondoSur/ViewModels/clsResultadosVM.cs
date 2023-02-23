using Entidades;
using PregunFondoSur.models;
using PregunFondoSur.ViewModels.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregunFondoSur.ViewModels
{
    [QueryProperty(nameof(DatosPartida), "datosPartida")]
    public class clsResultadosVM : clsVMBase
    {
        #region Atributos
        private clsDatosResultadoPartida datosPartida;
        private clsUsuario usuarioLocal;
        private clsUsuario usuarioRival;
        private List<clsCategoriasMaui> listadoCategoriasLocal;
        private List<clsCategoriasMaui> listadoCategoriasRival;
        #endregion

        #region Propiedades
        public clsDatosResultadoPartida DatosPartida { get { return datosPartida; } 
            set { datosPartida = value;
                UsuarioLocal = datosPartida.usuarioLocal;
                UsuarioRival = datosPartida.usuarioRival;
                ListadoCategoriasLocal = datosPartida.categoriasUsuarioLocal;
                ListadoCategoriasRival = datosPartida.categoriasUsuarioRival;
                   NotifyPropertyChanged(nameof(DatosPartida));
            } }
        public clsUsuario UsuarioLocal { get { return usuarioLocal; } 
            set { usuarioLocal = value;
                NotifyPropertyChanged(nameof(UsuarioLocal));} }

        public clsUsuario UsuarioRival
        {
            get { return usuarioRival; }
            set
            {
                usuarioRival = value;
                NotifyPropertyChanged(nameof(UsuarioRival));
            }
        }
        public List<clsCategoriasMaui> ListadoCategoriasLocal
        {
            get { return listadoCategoriasLocal; }
            set
            {
                listadoCategoriasLocal = value;
                NotifyPropertyChanged(nameof(ListadoCategoriasLocal));
            }
        }
        public List<clsCategoriasMaui> ListadoCategoriasRival
        {
            get { return listadoCategoriasRival; }
            set
            {
                listadoCategoriasRival = value;
                NotifyPropertyChanged(nameof(ListadoCategoriasRival));
            }
        }
        #endregion

        #region Constructores
        public clsResultadosVM()
        {

        }
        #endregion 


     
    }
}
