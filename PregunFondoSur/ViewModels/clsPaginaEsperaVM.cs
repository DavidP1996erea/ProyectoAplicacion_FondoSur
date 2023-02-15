using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregunFondoSur.ViewModels
{
    public class clsPaginaEsperaVM
    {
        #region Atributos
        private clsUsuario usuario;
        private bool rivalConectado;
        #endregion

        #region Propiedades
        public clsUsuario Usuario { get { return usuario; } }
        #endregion

        #region Constructores
        public clsPaginaEsperaVM() { 
        rivalConectado = false;
        }
        #endregion

        #region Metodos
        

        #endregion

        #region MetodosSignalR
        /// <summary>
        /// Metodos paletas2
        /// </summary>
        private void enviarBool() { }


        /// <summary>
        /// Metoddo paletas
        /// </summary>
        private async void recibirBool(bool rivalConenctado) {

            var navigationParameter = new Dictionary<string, object>
            {
                { "Usuario", Usuario }
            };
            await Shell.Current.GoToAsync($"PaginaEleccionCategoria", navigationParameter);
        }
        #endregion
    }
}
