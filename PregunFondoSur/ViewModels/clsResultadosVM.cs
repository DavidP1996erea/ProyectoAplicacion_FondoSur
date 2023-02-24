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
        private String mensajeGanadoPerdido;
        private String imagenResultadoLocal;
        private String imagenResultadoRival;
        #endregion

        #region Propiedades
        public clsDatosResultadoPartida DatosPartida
        {
            get { return datosPartida; }
            set
            {
                datosPartida = value;
                UsuarioLocal = datosPartida.usuarioLocal;
                UsuarioRival = datosPartida.usuarioRival;
                ListadoCategoriasLocal = datosPartida.categoriasUsuarioLocal;
                ListadoCategoriasRival = datosPartida.categoriasUsuarioRival;
                NotifyPropertyChanged(nameof(DatosPartida));
            }
        }
        public clsUsuario UsuarioLocal
        {
            get { return usuarioLocal; }
            set
            {
                usuarioLocal = value;
                NotifyPropertyChanged(nameof(UsuarioLocal));
            }
        }

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
                comprobarGanado();
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

        public String MensajeGanadoPerdido
        {
            get { return mensajeGanadoPerdido; }
            set { mensajeGanadoPerdido = value; }
        }

        public String ImagenResultadoLocal
        {
            get { return imagenResultadoLocal; }
            set { imagenResultadoLocal = value; }
        }
        public String ImagenResultadoRival
        {
            get { return imagenResultadoRival; }
            set { imagenResultadoRival = value; }
        }
        #endregion

        #region Constructores
        public clsResultadosVM()
        {
          
        }
        #endregion

        #region Metodos
        public void comprobarGanado()
        {
            int contadorCategoriasAcertadas = 0;
            for ( int i =0; i < listadoCategoriasLocal.Count; i++)
            {
                if (listadoCategoriasLocal[i].EstaAcertada)
                {
                    contadorCategoriasAcertadas++;
                }
            }
            if (contadorCategoriasAcertadas == 5)
            {
                mensajeGanadoPerdido = "Felicidades Has Ganado";
                imagenResultadoLocal = "medal.png";
                imagenResultadoRival = "platmedal.png";
            }
            else{
                mensajeGanadoPerdido = "Lo Sentimos, Has Perdido";
                imagenResultadoRival = "medal.png";
                imagenResultadoLocal = "platmedal.png";
            }
            NotifyPropertyChanged(MensajeGanadoPerdido);
            NotifyPropertyChanged(ImagenResultadoLocal);
            NotifyPropertyChanged(ImagenResultadoRival);
            
        }

        #endregion

    }
}
