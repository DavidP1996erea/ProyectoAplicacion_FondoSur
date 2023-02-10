using System;
using Entidades;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PregunFondoSur.ViewModels.Utilidades;

namespace PregunFondoSur.ViewModels
{
    public class clsLoginVM : clsVMBase
    {
        #region Atributos
        clsUsuario usuario;
        DelegateCommand logInCommand;
        #endregion

        #region Propiedades
        clsUsuario Usuario { get { return usuario; } 
            set { usuario = value;
                logInCommand.RaiseCanExecuteChanged();
            } }
        DelegateCommand LogInCommand { get { return logInCommand; } }
        #endregion

        #region Constructores
        public clsLoginVM() {
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
            bool pulsable=false;
            if (usuario.userName!= "" && usuario.password!="" && usuario.imagen!="") {
                pulsable=true;
            }
            return pulsable;
        }

        /// <summary>
        /// Metodo que al pulsar el botón de login, manda al usuario a la sala de espera recogiendo sus datos
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void logInCommand_Execute()
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
