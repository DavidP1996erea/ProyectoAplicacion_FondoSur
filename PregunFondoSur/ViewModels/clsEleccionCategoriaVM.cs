using Entidades;
using Java.Security;
using PregunFondoSur.models;
using PregunFondoSur.ViewModels.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregunFondoSur.ViewModels
{
    public class clsEleccionCategoriaVM : clsVMBase
    {
        #region Atributos
        private clsUsuario usuarioLocal;
        private clsUsuario usuarioRival;
        private List<clsCategorias> listaCategoriasLocal;
        private List<clsCategorias> listaCategoriasRival;
        private bool tuTurno;
        private bool estaGirando;
        private DelegateCommand girarRuletaCommand;
        #endregion

        #region Propiedades

        public clsUsuario UsuarioLocal
        {
            get { return usuarioLocal; }
            set { usuarioLocal = value; }
        }
        public clsUsuario UsuarioRival
        {
            get { return usuarioRival; }
            set { usuarioRival = value; }
        }
        public List<clsCategorias> ListaCategoriasLocal
        {
            get { return listaCategoriasLocal; }
            set { listaCategoriasLocal = value; }
        }
        public List<clsCategorias> ListaCategoriasRival
        {
            get { return listaCategoriasRival; }
            set { listaCategoriasRival = value; }
        }

        public bool TuTurno
        {
            get { return tuTurno; }
            set { tuTurno = value;
                girarRuletaCommand.RaiseCanExecuteChanged();
                    //TODO METODO QUE AVISA
                 }
        }
        #endregion

        #region Constructores
        public clsEleccionCategoriaVM() {
            listaCategoriasLocal=clsObtenerListadoCategorias.obtenerListadoCompletoCategorias();
            listaCategoriasRival = clsObtenerListadoCategorias.obtenerListadoCompletoCategorias();
            girarRuletaCommand = new DelegateCommand(girarRuletaCommand_Executed, girarRuletaCommand_CanExecuted);
        }


        #endregion

        #region Comandos
        private bool girarRuletaCommand_CanExecuted()
        {
            bool pulsable=false;
            if (tuTurno)
            {
                if (!estaGirando) { 
                    pulsable=true;
              }
            }
            return false;
        }

        private void girarRuletaCommand_Executed()
        {
            //Girar chuleta
        }
        #endregion
    }
}
