using Entidades;
using PregunFondoSur.models;
using PregunFondoSur.ViewModels.Utilidades;

namespace PregunFondoSur.ViewModels
{
    public class clsResultadosVM : clsVMBase, IQueryAttributable
    {
        #region Atributos
        private clsDatosResultadoPartida datosPartida { get; set; }
        private clsUsuario usuarioGanador;
        private clsUsuario usuarioPerdedor;
        private List<clsCategoriasMaui> listadoCategoriasLocal;
        private String mensajeGanadoPerdido;
        private Color colorMensaje;
        #endregion

        #region Propiedades
        public clsUsuario UsuarioGanador
        {
            get { return usuarioGanador; }
            set
            {
                usuarioGanador = value;
                NotifyPropertyChanged();
            }
        }

        public clsUsuario UsuarioPerdedor
        {
            get { return usuarioPerdedor; }
            set
            {
                usuarioPerdedor = value;
                NotifyPropertyChanged();
            }
        }

        public String MensajeGanadoPerdido
        {
            get { return mensajeGanadoPerdido; }
            set { mensajeGanadoPerdido = value; 
                NotifyPropertyChanged(); }
        }

        public Color ColorMensaje
        {
            get { return colorMensaje; }
            set 
            { 
              colorMensaje = value;
              NotifyPropertyChanged(); 
            }
        }

        #endregion

        #region Constructores
        public clsResultadosVM()
        {

        }
        #endregion

        #region Metodos
        /// <summary>
        /// Metodo que comprueba si has ganado la partida, y segun el resultado
        /// mostrara los datos correspondientes.
        /// </summary>
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
                MensajeGanadoPerdido = "CONGRATULATIONS YOU HAVE WON";
                UsuarioGanador = datosPartida.usuarioLocal;
                UsuarioPerdedor = datosPartida.usuarioRival;
                ColorMensaje = Color.Parse("Efb810");
            }
            else{
                MensajeGanadoPerdido = "YOU HAVE BEEN DEFEATED";
                UsuarioPerdedor = datosPartida.usuarioLocal;
                UsuarioGanador = datosPartida.usuarioRival;
                ColorMensaje = Color.Parse("#DF0101");
            }
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            datosPartida = query["datosPartida"] as clsDatosResultadoPartida;
            listadoCategoriasLocal = datosPartida.categoriasUsuarioLocal;
            comprobarGanado();
        }

        #endregion

    }
}
