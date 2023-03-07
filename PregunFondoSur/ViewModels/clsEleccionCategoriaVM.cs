using Entidades;
using Microsoft.AspNetCore.SignalR.Client;
using Models;
using PregunFondoSur.models;
using PregunFondoSur.ViewModels.Utilidades;
using Services;

namespace PregunFondoSur.ViewModels
{

    [QueryProperty(nameof(UsuarioLocal), "usuarioLocal")]
    [QueryProperty(nameof(CategoriaAcertada), "categoria")]
    public class clsEleccionCategoriaVM : clsVMBase
    {
        #region Atributos
        private clsUsuario usuarioLocal;
        private clsUsuario usuarioRival;
        private clsCategoriasMaui categoriaAcertada;
        private List<clsCategoriasMaui> listaCategoriasLocal;
        private List<clsCategoriasMaui> listaCategoriasRival;
        private List<clsPreguntas> listadoPreguntasFilms;
        private List<clsPreguntas> listadoPreguntasMusic;
        private List<clsPreguntas> listadoPreguntashistory;
        private List<clsPreguntas> listadoPreguntasFood;
        private List<clsPreguntas> listadoPreguntasScience;
        private clsPreguntas preguntaEnviar;
        //Booleano en el que se guarda si la ruleta esta girando o no para el canExecute de girarRuletaCommand
        private bool estaGirando;
        private DelegateCommand girarRuletaCommand;
        private Color colorFondoUsuario;

        private HubConnection miConexion;
        private int categoriaDeLaRuleta;
        private bool boolRuletaDisponible;
        private bool boolTextoEspera;
        #endregion

        #region Propiedades



        public int CategoriaDelaRuleta
        {
            get { return categoriaDeLaRuleta; }
            set
            {
                categoriaDeLaRuleta = value;
                NotifyPropertyChanged();
            }
        }


        //TODO ARREGLAR.
        public clsCategoriasMaui CategoriaAcertada
        {
            get { return categoriaAcertada; }
            set
            {
                categoriaAcertada = value;
                asignarCategoriaAcertadaAsync();
            }
        }
        public clsUsuario UsuarioLocal
        {
            get { return usuarioLocal; }

            set
            {
                usuarioLocal = value;

                // Se comprueba que antes de realizar las acciones el usuario no esté a null
                if (usuarioLocal != null)
                {
                    enviarUsuario();
                    NotifyPropertyChanged();
                    establecerColorFondo();
                    girarRuletaCommand.RaiseCanExecuteChanged();


                }
            }
        }

        public clsUsuario UsuarioRival
        {
            get { return usuarioRival; }
            set
            {
                usuarioRival = value;
                NotifyPropertyChanged();
            }
        }
        public List<clsCategoriasMaui> ListaCategoriasLocal
        {
            get { return listaCategoriasLocal; }
            set
            {
                listaCategoriasLocal = value;
            }
        }
        public List<clsCategoriasMaui> ListaCategoriasRival
        {
            get { return listaCategoriasRival; }
            set { listaCategoriasRival = value; }
        }

        public Color ColorFondoUsuario
        {
            get { return colorFondoUsuario; }
            set { colorFondoUsuario = value; NotifyPropertyChanged(); }
        }

        public DelegateCommand GirarRuletaCommand
        {
            get { return girarRuletaCommand; }
        }

        public bool BoolRuletaDisponible
        {
            get { return boolRuletaDisponible; }
            set
            {
                boolRuletaDisponible = value;
                NotifyPropertyChanged();
            }
        }

        public bool BoolTextoEspera
        {
            get { return boolTextoEspera; }
            set
            {
                boolTextoEspera = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Constructores
        public clsEleccionCategoriaVM()
        {
            // Se rellenan todas las listas con preguntas
            obtenerListados();


            listaCategoriasLocal = clsObtenerListadoCategorias.obtenerListadoCompletoCategorias();
            listaCategoriasRival = clsObtenerListadoCategorias.obtenerListadoCompletoCategorias();

            girarRuletaCommand = new DelegateCommand(girarRuletaCommand_Executed, girarRuletaCommand_CanExecuted);
            // Se crea la conexión con el servidor.
            miConexion = new HubConnectionBuilder().WithUrl("https://proyectofondosur.azurewebsites.net/eleccionCategoriasHub").Build();


            recibirUsuario();
            recibirListadoCategorias();
            recibirCambiarTurno();
        }

        #endregion


        #region Métodos SignalR


        /// <summary>
        /// Comprueba que la conexión está establecida
        /// </summary>
        /// <returns></returns>
        private async Task comprobarConexion()
        {
            if (miConexion.State == HubConnectionState.Disconnected)
            {
                miConexion = new HubConnectionBuilder().WithUrl("https://proyectofondosur.azurewebsites.net/eleccionCategoriasHub").Build();
            }
        }

        /// <summary>
        /// Método del signalR que envía al usuario local 
        /// </summary>
        /// <returns></returns>
        private async Task enviarUsuario()
        {
            await comprobarConexion();
            await miConexion.InvokeCoreAsync("enviarUsuario", args: new[] { UsuarioLocal });


        }

        /// <summary>
        /// Se recibe los datos del usuario local y en caso de que el nombre sea diferente, significa
        /// que es el usuario rival, por lo que se setea el usuario rival con datosUsuario
        /// </summary>
        /// <returns></returns>
        private async Task recibirUsuario()
        {
            int cont = 2;
            miConexion.On<clsUsuario>("recibirUsuario", async (datosUsuario) =>
            {

                if (cont > 0)
                {
                    if (UsuarioLocal.userName == datosUsuario.userName)
                    {
                        if (UsuarioLocal.tuTurno != true)
                        {
                            UsuarioLocal = datosUsuario;
                            NotifyPropertyChanged(nameof(UsuarioLocal));
                        }
                    }
                    else
                    {
                        if (datosUsuario.userName != null || datosUsuario.userName != "")
                        {
                            UsuarioRival = datosUsuario;
                            NotifyPropertyChanged(nameof(UsuarioRival));
                        }
                    }
                    cont--;
                }

            });
            await miConexion.StartAsync();

        }

        /// <summary>
        /// Método que envía el valor del turno
        /// </summary>
        /// <returns></returns>
        private async Task enviarCambiarValorTurno()
        {
            await comprobarConexion();
            UsuarioLocal.tuTurno = false;
            NotifyPropertyChanged(nameof(UsuarioLocal));
            await miConexion.InvokeCoreAsync("enviarCambiarValorTurno", args: new[] { "true", UsuarioLocal.nombreSala });
        }

        /// <summary>
        /// Cuando es llamado, cambia el valor del turno del usuario local a true
        /// </summary>
        /// <returns></returns>
        private async Task recibirCambiarTurno()
        {
            miConexion.On<String>("recibirCambiarTurno", (turno) =>
            {
                clsUsuario usuarioTemporal = UsuarioLocal;
                usuarioTemporal.tuTurno = true;
                UsuarioLocal = usuarioTemporal;
                NotifyPropertyChanged(nameof(UsuarioLocal));
            });

            await miConexion.StartAsync();
        }


        /// <summary>
        /// Método que envía el listado de categorías local
        /// </summary>
        /// <returns></returns>

        private async Task enviarListadoCategorias()
        {
            await comprobarConexion();
            List<clsCategorias> listadoCategoriasEnviar = new List<clsCategorias>();

            for (int i = 0; i < ListaCategoriasLocal.Count; i++)
            {

                listadoCategoriasEnviar.Add(ListaCategoriasLocal[i]);

            }
            await miConexion.InvokeAsync("EnviarListadoCategorias", listadoCategoriasEnviar, UsuarioLocal.nombreSala);
        }

        /// <summary>
        /// Recibe el listado de categorías del rival y lo setea. También se comprueba si el rival ha ganado
        /// </summary>
        /// <returns></returns>
        private async Task recibirListadoCategorias()
        {

            miConexion.On<List<clsCategorias>>("recibirListadoCategorias", (listadoCategorias) =>
            {


                for (int i = 0; i < listadoCategorias.Count; i++)
                {
                    ListaCategoriasRival[i].ImagenMostrada = listadoCategorias[i].ImagenMostrada;
                    List<clsCategoriasMaui> listaAuxiliar = new List<clsCategoriasMaui>(ListaCategoriasRival);
                    ListaCategoriasRival = listaAuxiliar;

                }
                NotifyPropertyChanged(nameof(ListaCategoriasRival));
                comprobarFinalizarPartidaRival(listadoCategorias);
            });

            await miConexion.StartAsync();
        }






        #endregion

        #region Metodos
        /// <summary>
        /// Método que al ser llamado rellena los listados de las diferentes categorías llamando una clase
        /// que conecta con la api y recoge todos los datos necesarios
        /// </summary>
        private async void obtenerListados()
        {
            listadoPreguntasFilms = await clsObtenerListadoPreguntasPorCategoria.obtenerListadoPreguntasFilmDAL();
            listadoPreguntasMusic = await clsObtenerListadoPreguntasPorCategoria.obtenerListadoPreguntasMusicDAL();
            listadoPreguntashistory = await clsObtenerListadoPreguntasPorCategoria.obtenerListadoPreguntasHistoryDAL();
            listadoPreguntasFood = await clsObtenerListadoPreguntasPorCategoria.obtenerListadoPreguntasFoodDAL();
            listadoPreguntasScience = await clsObtenerListadoPreguntasPorCategoria.obtenerListadoPreguntasScienceDAL();
        }


        /// <summary>
        /// Este método se llama cuando la partida a terminado. Primero se rellena un objeto de un model que contendrá todos los datos
        /// necesarios para la vista de partida finalizada. Luego envía a través del shell este objeto al viewmodel de la última vista
        /// </summary>
        private async void finalizarJuego()
        {
            clsDatosResultadoPartida datosPartida = new clsDatosResultadoPartida(usuarioLocal, usuarioRival, listaCategoriasLocal, listaCategoriasRival);
            var navigationParameter = new Dictionary<string, object>
            {
                { "datosPartida", datosPartida }
            };
            await Shell.Current.GoToAsync("PaginaFinalizacion", navigationParameter);
        }


        /// <summary>
        /// Método canexecuted que controlará cuando se puede pulsar la ruleta. Siempre
        /// que el turno del usuario sea true, podrá pulsar la ruleta
        /// </summary>
        /// <returns></returns>
        private bool girarRuletaCommand_CanExecuted()
        {
            bool pulsable = false;

            if (UsuarioLocal != null)
            {
                if (UsuarioLocal.tuTurno)
                {
                    pulsable = true;
                }
            }
            return pulsable;
        }


        /// <summary>
        /// Método donde se genera una pregunta, y se envía a otra vista donde el usuario
        /// deberá seleccionar la respuesta necesaria.
        /// </summary>
        private async void girarRuletaCommand_Executed()
        {
            estaGirando = true;
            girarRuletaCommand.RaiseCanExecuteChanged();


            await Task.Delay(TimeSpan.FromMilliseconds(2300));
            preguntaEnviar = generarPregunta();

            var navigationParameter = new Dictionary<string, object>
            {
                { "pregunta", preguntaEnviar }

            };


            await Shell.Current.GoToAsync("PaginaPregunta", navigationParameter);

        }

        /// <summary>  
        /// Método que ajusta el color del recuadro del usuario en funcion al booleano TuTurno, el cual almacena si es el turno
        /// del usuario local o no.
        /// Precondiciones: Ninguna
        /// Postcondiciones: Ninguna  
        /// </summary>
        private void establecerColorFondo()
        {
            if (usuarioLocal.tuTurno)
            {
                ColorFondoUsuario = Color.Parse("White");
                NotifyPropertyChanged(nameof(ColorFondoUsuario));
                BoolRuletaDisponible = true;
                BoolTextoEspera = false;
            }
            else
            {
                ColorFondoUsuario = Color.Parse("Gray");
                NotifyPropertyChanged(nameof(ColorFondoUsuario));
                BoolRuletaDisponible = false;
                BoolTextoEspera = true;
            }
        }

        /// <summary>
        /// Método que inserta la categoria acertada mandanda por el shell a través de recorrer la lista de categorias del 
        /// usuarioLocal, y cuando la encuentre, hará un set a True al booleano EstaAcertada de la clase Categoria y igualara la imagen
        /// mostrada a la imagen correspondiente
        /// Precondiciones: Ninguna
        /// Postcondiciones: Ninguna
        /// </summary>
        private async Task asignarCategoriaAcertadaAsync()
        {
            if (categoriaAcertada.EstaAcertada)
            {
                for (int i = 0; i < listaCategoriasLocal.Count; i++)
                {
                    if (categoriaAcertada.Nombre.Contains(listaCategoriasLocal[i].Nombre))
                    {
                        listaCategoriasLocal[i] = categoriaAcertada;
                    }
                }
                List<clsCategoriasMaui> listaAuxiliar = new List<clsCategoriasMaui>(listaCategoriasLocal);
                listaCategoriasLocal = listaAuxiliar;
                NotifyPropertyChanged(nameof(ListaCategoriasLocal));
                await enviarListadoCategorias();
                UsuarioLocal.tuTurno = true;
                establecerColorFondo();
                girarRuletaCommand.RaiseCanExecuteChanged();
                await Task.Delay(TimeSpan.FromMilliseconds(300));
                comprobarFinalizarPartida(listaCategoriasLocal);
            }
            else
            {
                await enviarCambiarValorTurno();
                establecerColorFondo();
            }
        }

        /// <summary>
        /// Método que devuelve un objeto de la clase clsPreguntas. Se comprueba el número seleccionado
        /// por la ruleta y se crea una pregunta aleatoria según que categoría es
        /// </summary>
        /// <returns></returns>
        private clsPreguntas generarPregunta()
        {
            clsPreguntas preguntaElegida = new clsPreguntas();


            if (CategoriaDelaRuleta >= 0 && CategoriaDelaRuleta <= 8)
            {
                preguntaElegida = devolverPreguntaPorCategoria(listadoPreguntashistory);
            }
            else if (CategoriaDelaRuleta >= 9 && CategoriaDelaRuleta <= 17)
            {
                preguntaElegida = devolverPreguntaPorCategoria(listadoPreguntasScience);
            }
            else if (CategoriaDelaRuleta >= 18 && CategoriaDelaRuleta <= 26)
            {
                preguntaElegida = devolverPreguntaPorCategoria(listadoPreguntasFood);
            }
            else if (CategoriaDelaRuleta >= 27 && CategoriaDelaRuleta <= 35)
            {
                preguntaElegida = devolverPreguntaPorCategoria(listadoPreguntasMusic);
            }
            else if (CategoriaDelaRuleta >= 36 && CategoriaDelaRuleta <= 44)
            {
                preguntaElegida = devolverPreguntaPorCategoria(listadoPreguntasFilms);
            }


            return preguntaElegida;
        }

        /// <summary>
        /// Devuelve una clsPreguntas y recibe como parámetro un listado de preguntas. Primero se comprueba
        /// que la pregunta no esté respondida, para ello se usa un while donde se usa la propiedad isNiche.
        /// Cuando se escoge una pregunta que no está respondida se setea el objeto creao anteriormente de
        /// clsPreguntas a la pregunta escogida aleatoriamente y se devuelve.
        /// </summary>
        /// <param name="listadoPreguntas"></param>
        /// <returns></returns>
        private static clsPreguntas devolverPreguntaPorCategoria(List<clsPreguntas> listadoPreguntas)
        {

            Random random = new Random();
            clsPreguntas preguntaSeleccionada = new clsPreguntas();
            int idPregunta = random.Next(0, 39);


            while (listadoPreguntas[idPregunta].isNiche)
            {
                idPregunta = random.Next(0, 39);
            }

            preguntaSeleccionada = listadoPreguntas[idPregunta];

            return preguntaSeleccionada;
        }

        /// <summary>
        /// Método que recibe como parámetro un listado de categorías. Lo recorre y va comprobando
        /// en cada iteración si esa categoría está acertada, en caso de estar todas acertadas
        /// se llamará al método finalizarJuego.
        /// </summary>
        /// <param name="listaCategoria"></param>
        public void comprobarFinalizarPartida(List<clsCategoriasMaui> listaCategoria)
        {
            int contadorPreguntasAcertadas = 0;
            for (int i = 0; i < listaCategoria.Count; i++)
            {
                if (listaCategoria[i].EstaAcertada)
                {
                    contadorPreguntasAcertadas++;
                }
            }
            if (contadorPreguntasAcertadas == 5)
            {
                finalizarJuego();
            }
        }

        /// <summary>
        /// Igual que el método anterior pero comprueba que el rival haya ganado
        /// </summary>
        /// <param name="listaCategoria"></param>
        public void comprobarFinalizarPartidaRival(List<clsCategorias> listaCategoria)
        {
            int contadorPreguntasAcertadas = 0;
            for (int i = 0; i < listaCategoria.Count; i++)
            {
                if (listaCategoria[i].EstaAcertada)
                {
                    contadorPreguntasAcertadas++;
                }
            }
            if (contadorPreguntasAcertadas == 5)
            {
                finalizarJuego();
            }
        }


        #endregion
    }
}
