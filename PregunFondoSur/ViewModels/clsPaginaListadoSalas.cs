using Entidades;
using Microsoft.AspNetCore.SignalR.Client;
using PregunFondoSur.ViewModels.Utilidades;

namespace PregunFondoSur.ViewModels
{
    [QueryProperty(nameof(Usuario), "Usuario")]
    public class clsPaginaListadoSalas : clsVMBase
    {
        #region Atributos
        private clsUsuario usuario;
        private List<string> listadoDeSalas;
        private string salaSelecionada;
        private readonly HubConnection miConexion;
        #endregion

        #region Propiedades
        public clsUsuario Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
                NotifyPropertyChanged();
            }
        }
        public List<string> ListadoDeSalas
        {
            get { return listadoDeSalas; }
            set { listadoDeSalas = value; }
        }
        public string SalaSelecionada
        {
            get { return salaSelecionada; }
            set { 
                salaSelecionada = value; 
                if (salaSelecionada != null) { 
                    enviarASalaEspera(salaSelecionada);
                } 
            }
        }
        #endregion


        #region Constructores
        public clsPaginaListadoSalas()
        {
            miConexion = new HubConnectionBuilder().WithUrl("https://proyectofondosur.azurewebsites.net/salasHub").Build();

            recibirListadoSalas();
            enviarListadoSalas();
        }

        #endregion

        #region Métodos SignalR
        /// <summary>
        /// Envía un listado
        /// </summary>
        /// <returns></returns>
        private async Task enviarListadoSalas()
        {
            List<String> lista = new List<string>();
            await miConexion.InvokeCoreAsync("enviarListadoSalas", args: new[] { lista });
        }

        /// <summary>
        /// Recibe un listado de salas
        /// </summary>
        /// <returns></returns>
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

        #region Metodos

        /// <summary>
        /// Envía un usuario a la página PaginaEspera
        /// </summary>
        /// <param name="salaSelecionada"></param>
        public async void enviarASalaEspera(string salaSelecionada)
        {
            Usuario.nombreSala= salaSelecionada;
            var navigationParameter = new Dictionary<string, object>
            {
                { "Usuario", Usuario }
                
            };
            await Shell.Current.GoToAsync("PaginaEspera", navigationParameter);
        }
        #endregion
    }
}
