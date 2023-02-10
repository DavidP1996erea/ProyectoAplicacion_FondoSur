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

        private bool logInCommand_CanExecute()
        {
            bool pulsable=false;
            if (usuario.userName ==) { }
            return pulsable;
        }

        private void logInCommand_Execute()
        {
            throw new NotImplementedException();
        }
        #endregion

#
    }
}
