
using Entidades;
using Microsoft.VisualBasic;
using Models;
using PregunFondoSur.ViewModels.Utilidades;

namespace PregunFondoSur.ViewModels
{
    public class clsLoginVM : clsVMBase
    {
        #region Atributos
        private clsUsuario usuario;
        private string nickname, imagen;
        private DelegateCommand logInCommand;
        private DelegateCommand crearSalaCommand;
        #endregion

        #region Propiedades

        public string Nickname
        {
            get { return nickname; }
            set { nickname = value;
                logInCommand.RaiseCanExecuteChanged();
            }
        }

        public string Imagen
        {
            get { return imagen; }
            set { imagen = value;
                logInCommand.RaiseCanExecuteChanged();
            }
        }

        public clsUsuario Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
            }
        }
        public DelegateCommand LogInCommand { get { return logInCommand; } }
        public DelegateCommand CrearSalaCommand { get { return crearSalaCommand; } }
        #endregion

        #region Constructores
        public clsLoginVM()
        {
            usuario = new clsUsuario();
            logInCommand = new DelegateCommand(logInCommand_Execute, logInCommand_CanExecute);
            crearSalaCommand = new DelegateCommand(crearSalaCommand_Execute);
        }

     

        /// <summary>
        /// Método que revisa si el usuario ha introducido los datos necesarios en los entrys del la vista como para loggearse
        /// Precondiciones:Ninguna
        /// Postcondiciones:Ninguna
        /// </summary>
        /// <returns></returns>
        private bool logInCommand_CanExecute()
        {
            bool pulsable = false;
            if ((Nickname != "" && Nickname != null) && (Imagen != "" && Imagen != null))
            {
                pulsable = true;
            }
            NotifyPropertyChanged("Usuario");
            return pulsable;
        }

        #endregion

        /// <summary>
        /// Metodo que al pulsar el botón de login, manda al usuario a la sala de espera recogiendo sus datos
        /// </summary>
        private async void logInCommand_Execute()
        {
            Usuario.userName = Nickname;
            Usuario.imagen = Imagen;
            var navigationParameter = new Dictionary<string, object>
            {
                {"Usuario", Usuario }
            };
            await Shell.Current.GoToAsync("PaginaEspera", navigationParameter);
        }


        private async void crearSalaCommand_Execute()
        {
            string nombreSala = await App.Current.MainPage.DisplayPromptAsync("Crear sala", "Introduce el nombre de la sala");
            Usuario.nombreSala = nombreSala;
            clsListadoSalas.ListadoSalas.Add(usuario.nombreSala);
        }


    }
}
