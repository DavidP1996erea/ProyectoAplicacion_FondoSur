
using Entidades;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.VisualBasic;
using Models;
using PregunFondoSur.ViewModels.Utilidades;

namespace PregunFondoSur.ViewModels
{
    public class clsLoginVM : clsVMBase
    {
        #region Atributos
        private clsUsuario usuario;
        private string nickname, imagen;
        private DelegateCommand logInCommand;
        private DelegateCommand crearSalaCommand;
        private readonly HubConnection miConexion;
        private bool boolMensajeCampos;
        #endregion

        #region Propiedades

        public bool BoolMensajeCampos
        {
            get
            {
                return boolMensajeCampos;
            }
            set
            {
                boolMensajeCampos = value;
                NotifyPropertyChanged();
            }
        }

        public string Nickname
        {
            get { return nickname; }
            set
            {
                nickname = value;
                logInCommand.RaiseCanExecuteChanged();
            }
        }

        public string Imagen
        {
            get { return imagen; }
            set
            {
                imagen = value;
                logInCommand.RaiseCanExecuteChanged();
            }
        }

        public clsUsuario Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
            }
        }
        public DelegateCommand LogInCommand { get { return logInCommand; } }
        public DelegateCommand CrearSalaCommand { get { return crearSalaCommand; } }
        #endregion

        #region Constructores
        public clsLoginVM()
        {
            BoolMensajeCampos = false;
            usuario = new clsUsuario();
            logInCommand = new DelegateCommand(logInCommand_Execute);
            crearSalaCommand = new DelegateCommand(crearSalaCommand_Execute);

            miConexion = new HubConnectionBuilder().WithUrl("https://proyectofondosur.azurewebsites.net/salasHub").Build();

            recibirCrearSala();
        }

        #endregion

        /// <summary>
        /// Metodo que al pulsar el botón de login, manda al usuario a la sala de espera recogiendo sus datos
        /// </summary>
        private async void logInCommand_Execute()
        {
            if ((Nickname != "" && Nickname != null) && (Imagen != "" && Imagen != null))
            {
                Usuario.userName = Nickname;
                Usuario.imagen = Imagen;
                var navigationParameter = new Dictionary<string, object>
            {
                {"Usuario", Usuario }
            };
                await Shell.Current.GoToAsync("PaginaListadoSalas", navigationParameter);
                BoolMensajeCampos = false;
            }
            else
            {
                BoolMensajeCampos = true;
            }
        }


        /// <summary>
        /// Método que recibe una cadena nombreSala para pasarla por signal R y crear una nueva sala.
        /// </summary>
        /// <param name="nombreSala"></param>
        /// <returns></returns>
        #region Métodos SignalR
        private async Task crearSala(string nombreSala)
        {
            await miConexion.InvokeCoreAsync("crearSala", args: new[] { nombreSala });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task recibirCrearSala()
        {
            miConexion.On<String>("recibirCrearSala", (nombreSala) =>
            {
                
       
            });

            await miConexion.StartAsync();
        }

        #endregion


        /// <summary>
        /// Método que comprueba que las propiedades públicas Nickname, Imagen y la propiedad privada
        /// nombre sala no son nulas ni tienen un valor vacio para así preguntar al usuario por el nombre de la sala
        /// y crearla, uniendose a ella y pasando a la página PaginaEspera enviandole un objeto de tipo Usuario con los
        /// valores recogidos de la vista.
        /// </summary>
        private async void crearSalaCommand_Execute()
        {
            if (Nickname != null && Nickname != "" && (Imagen != "" && Imagen != null))
            {
                string nombreSala = await App.Current.MainPage.DisplayPromptAsync("Crear sala", "Introduce el nombre de la sala");
                BoolMensajeCampos = false;

                if (nombreSala != null && nombreSala != "")
                {
                    await crearSala(nombreSala);

                    Usuario.userName = Nickname;
                    Usuario.imagen = Imagen;
                    Usuario.nombreSala = nombreSala;

                    var navigationParameter = new Dictionary<string, object>
                {
                    {"Usuario", Usuario }
                };
                    await Shell.Current.GoToAsync("PaginaEspera", navigationParameter);

                }
            }
            else
            {
                BoolMensajeCampos = true;
            }
        }
    }
}

