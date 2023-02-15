using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using PregunFondoSur.ViewModels.Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace PregunFondoSur.ViewModels.clsPreguntasVM
{
    public class clsPreguntasVM : clsVMBase
    {
        #region Atributos
        private clsPreguntas pregunta;
        private int tiempo;
        private DelegateCommand pulsarBotonCommand;
        #endregion

        #region Propiedades
        public clsPreguntas Pregunta { get { return pregunta; } set { pregunta = value; } }
        public int Tempo { get { return tiempo; } set { tiempo = value; } }
        public DelegateCommand PulsarBotonCommand { get { return pulsarBotonCommand; } }
        #endregion

        #region Constructores
        public clsPreguntasVM() {
            tiempo = 60;
            pulsarBotonCommand = new DelegateCommand( pulsarBotonCommand_Executed, pulsarBotonCommand_CanExecuted);
        }


        #endregion


        private bool pulsarBotonCommand_CanExecuted()
        {
            throw new NotImplementedException();
        }

        private void pulsarBotonCommand_Executed()
        {
            throw new NotImplementedException();
        }

        #region SignalR
        #endregion
    }
}

