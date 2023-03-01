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
    public class clsPaginaListadoSalas : clsVMBase
    {
        #region Atributos
        private List<string> listadoDeSalas;
        private readonly HubConnection miConexion;
        #endregion

        #region Propieades
        public List<string> ListadoDeSalas
        {
            get { return listadoDeSalas; }
            set { listadoDeSalas = value; }
        }
        #endregion


        #region Constructor es
        public clsPaginaListadoSalas()
        {            
            miConexion = new HubConnectionBuilder().WithUrl("http://localhost:5153/salasHub").Build();

            recibirListadoSalas();
            enviarListadoSalas();
        }

        #endregion

        #region Métodos SignalR

        private async Task enviarListadoSalas()
        {
            List<String> lista = new List<string>();
            await miConexion.InvokeCoreAsync("enviarListadoSalas", args: new[] { lista });
        }

        private async Task recibirListadoSalas()
        {
            miConexion.On<List<string>>("recibirListadoSalas", (listadoSalas) =>
            {

                listadoDeSalas = new List<string>(listadoSalas);

                NotifyPropertyChanged(nameof(ListadoDeSalas));
            });

            await miConexion.StartAsync();
        }

        #endregion
    }
}
