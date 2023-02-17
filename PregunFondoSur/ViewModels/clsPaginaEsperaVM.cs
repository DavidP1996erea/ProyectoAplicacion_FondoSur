using Entidades;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregunFondoSur.ViewModels
{
    [QueryProperty(nameof(Usuario), "Usuario")]
    public class clsPaginaEsperaVM
    {
        #region Atributos
        private clsUsuario usuario;
        private bool rivalConectado;
        private readonly HubConnection miConexion;
        #endregion

        #region Propiedades
        public clsUsuario Usuario { get { return usuario; } }
        #endregion

        #region Constructores
        public clsPaginaEsperaVM()
        {
            rivalConectado = false;


            // Se crea la conexión con el servidor
            miConexion = new HubConnectionBuilder().WithUrl("https://proyectofondosur.azurewebsites.net/salaEsperaHub").Build();



            recibirBool();
            enviarBool();
        }


        #endregion

        #region Metodos


        #endregion

        #region MetodosSignalR
        /// <summary>
        /// Metodos paletas2
        /// </summary>
        private async Task enviarBool()
        {

            await miConexion.InvokeCoreAsync("enviarBool", args: new[] { "true" });

        }


        /// <summary>
        /// Metoddo paletas
        /// </summary>
        private async Task recibirBool()
        {
            int cont = 1;
            miConexion.On<string>("recibirBool", async (hayRival) =>
            {

                if (cont > 0)
                {
                    cont--;
                    await enviarBool();
                    pasarPagina();
                }
             
            });

            await miConexion.StartAsync();

        }
        #endregion


        private async void pasarPagina()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Usuario", Usuario }
            };
            await Shell.Current.GoToAsync($"PaginaEleccionCategoria", navigationParameter);

        }

    }
}
