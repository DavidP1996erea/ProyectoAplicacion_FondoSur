using System;
using Entidades;
using PregunFondoSur.ViewModels.Utilidades;

namespace PregunFondoSur.ViewModels
{
    public class clsLoginVM : clsVMBase
    {
        #region Atributos
        private clsUsuario usuario;
        private DelegateCommand logInCommand;
        #endregion

        #region Propiedades
        public clsUsuario Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
                logInCommand.RaiseCanExecuteChanged();
            }
        }
        public DelegateCommand LogInCommand { get { return logInCommand; } }
        #endregion

        #region Constructores
        public clsLoginVM()
        {
            usuario = new clsUsuario();
            logInCommand = new DelegateCommand(logInCommand_Execute, logInCommand_CanExecute);
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
            if (usuario.userName != "" && usuario.password != "" && usuario.imagen != "")
            {
                pulsable = true;
            }
            return pulsable;
        }

        #endregion

        /// <summary>
        /// Metodo que al pulsar el botón de login, manda al usuario a la sala de espera recogiendo sus datos
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private async void logInCommand_Execute()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Usuario", Usuario }
            };
            await Shell.Current.GoToAsync($"PaginaEspera", navigationParameter);
        }

    }
}
