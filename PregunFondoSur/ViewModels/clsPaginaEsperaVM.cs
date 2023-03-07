using Entidades;
using Microsoft.AspNetCore.SignalR.Client;
using PregunFondoSur.ViewModels.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregunFondoSur.ViewModels
{
    [QueryProperty(nameof(Usuario), "Usuario")]
    public class clsPaginaEsperaVM : clsVMBase
    {
        #region Atributos
        private clsUsuario usuario;
        private bool rivalConectado;
        private readonly HubConnection miConexion;
        #endregion

        #region Propiedades
        public clsUsuario Usuario { get { return usuario; } 
            set { 
                usuario = value;
                NotifyPropertyChanged();
                if(usuario!= null)
                {
                    enviarBool();
                }
            } 
        }
        #endregion

        #region Constructores
        public clsPaginaEsperaVM()
        {
            rivalConectado = false;


            // Se crea la conexión con el servidor
            miConexion = new HubConnectionBuilder().WithUrl("https://proyectofondosur.azurewebsites.net/salaEsperaHub").Build();

            recibirBool();
            
        }


        #endregion

        #region Metodos


        #endregion

        #region MetodosSignalR
        /// <summary>
        /// Método que envía un usuario
        /// </summary>
        private async Task enviarBool()
        {

            await miConexion.InvokeCoreAsync("enviarBool", args: new[] { Usuario });

        }


        /// <summary>
        /// Recibe al usuario, y en caso de que sea un nombre diferene al usuario local
        /// significa que es el rival, por lo que se pasa de página.
        /// </summary>
        private async Task recibirBool()
        {
            int cont = 1;
            miConexion.On<clsUsuario>("recibirBool", async (usuarioEspera) =>
            {

                if (usuario.userName != usuarioEspera.userName)
                {
                    if (cont > 0)
                    {
                        cont--;
                        await enviarBool();
                        await pasarPagina();
                    }
                }
             
            });

            await miConexion.StartAsync();

        }
        #endregion

        /// <summary>
        /// Método que envía un usuario a la página PaginaEleccionCategoría
        /// </summary>
        /// <returns></returns>
        private async Task pasarPagina()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "usuarioLocal", usuario }
            };
            await Shell.Current.GoToAsync("PaginaEleccionCategoria", navigationParameter);

        }

    }
}
