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
        private bool tuTurno;
        //Booleano en el que se guarda si la ruleta esta girando o no para el canExecute de girarRuletaCommand
        private bool estaGirando;
        private DelegateCommand girarRuletaCommand;
        private String colorFondoUsuario;

        private readonly HubConnection miConexion;
        private int categoriaDeLaRuleta;
        #endregion

        #region Propiedades

        public int CategoriaDelaRuleta
        {
            get { return categoriaDeLaRuleta; }
            set { categoriaDeLaRuleta = value;
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
                asignarCategoriaAcertada();
            }
        }
        public clsUsuario UsuarioLocal { get { return usuarioLocal; } 
            
            set { usuarioLocal = value;
              
                if (usuarioLocal != null)
                {
                    NotifyPropertyChanged();
              
                    enviarUsuario();
                    girarRuletaCommand.RaiseCanExecuteChanged();
                    
                }
            } }

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
                comprobarVictoria();
            }
        }
        public List<clsCategoriasMaui> ListaCategoriasRival
        {
            get { return listaCategoriasRival; }
            set { listaCategoriasRival = value; }
        }
        /// <summary>
        /// oaoao
        /// </summary>
        public bool TuTurno
        {
            get { return tuTurno; }
            set
            {
                tuTurno = value;
                NotifyPropertyChanged();
                girarRuletaCommand.RaiseCanExecuteChanged();
                establecerColorFondo();
            }
        }

        public String ColorFondoUsuario
        {
            get { return colorFondoUsuario; }
            set { colorFondoUsuario = value; }
        }

        public DelegateCommand GirarRuletaCommand
        {
            get { return girarRuletaCommand; }
        }
        #endregion

        #region Constructores
        public clsEleccionCategoriaVM()
        {
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


        private async Task enviarUsuario()
        {
            await miConexion.InvokeCoreAsync("enviarUsuario", args: new[] { UsuarioLocal });
        }

        private async Task recibirUsuario()
        {
            int cont = 2;
            miConexion.On<clsUsuario>("recibirUsuario",  (datosUsuario) => {
                if (cont > 0)
                {
                    if (UsuarioLocal.userName == datosUsuario.userName)
                    {
                        UsuarioLocal = datosUsuario;
                        NotifyPropertyChanged(nameof(UsuarioLocal));
                        
                    }
                    else
                    {
                        UsuarioRival = datosUsuario;
                        NotifyPropertyChanged(nameof(UsuarioRival));
                    }
                    cont--;
                }
                    

            });
            await miConexion.StartAsync();

        }
        
        private async Task enviarCambiarValorTurno() {
            UsuarioLocal.tuTurno = false;
            NotifyPropertyChanged(nameof(UsuarioLocal));
            await miConexion.InvokeCoreAsync("enviarCambiarValorTurno", args: new[] { "true" });
        }

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
        


        private async Task enviarListadoCategorias()
        {
            List<clsCategorias> listadoCategoriasEnviar= new List<clsCategorias>(); 

            for(int i = 0; i< ListaCategoriasLocal.Count; i++) {

                listadoCategoriasEnviar.Add(ListaCategoriasLocal[i]);
            
            }
            await miConexion.InvokeCoreAsync("enviarListadoCategorias", args: new[] { listadoCategoriasEnviar });
        }

        private async Task recibirListadoCategorias()
        {

            miConexion.On<List<clsCategorias>>("recibirListadoCategorias", (listadoCategorias) =>
            {


                for (int i = 0; i < listadoCategorias.Count; i++)
                {

                    ListaCategoriasRival[i].ImagenMostrada = listadoCategorias[i].ImagenMostrada;

                }
                NotifyPropertyChanged(nameof(ListaCategoriasRival));

            });

            await miConexion.StartAsync();

        }





        #endregion

        #region Metodos
        private async void obtenerListados()
        {
            listadoPreguntasFilms = await clsObtenerListadoPreguntasPorCategoria.obtenerListadoPreguntasFilmDAL();
            listadoPreguntasMusic = await clsObtenerListadoPreguntasPorCategoria.obtenerListadoPreguntasMusicDAL();
            listadoPreguntashistory = await clsObtenerListadoPreguntasPorCategoria.obtenerListadoPreguntasHistoryDAL();
            listadoPreguntasFood = await clsObtenerListadoPreguntasPorCategoria.obtenerListadoPreguntasFoodDAL();
            listadoPreguntasScience = await clsObtenerListadoPreguntasPorCategoria.obtenerListadoPreguntasScienceDAL();

        }

        

        private void finalizarTurno() {
            tuTurno = false;
        }

        /// <summary>
        /// 
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

        private async void girarRuletaCommand_Executed()
        {
            estaGirando = true;
            girarRuletaCommand.RaiseCanExecuteChanged();

            ListaCategoriasLocal[2].ImagenMostrada = ListaCategoriasLocal[2].ImagenAcertada;
            enviarListadoCategorias();
            

            await Task.Delay(TimeSpan.FromMilliseconds(2300));
            preguntaEnviar = generarPregunta();
            //TODO Implementar Chuleta
            var navigationParameter = new Dictionary<string, object>
            {
                { "pregunta", preguntaEnviar }

            };


            enviarCambiarValorTurno();
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
            if (TuTurno)
            {
                colorFondoUsuario = "#00000000";
            }
            else
            {
                colorFondoUsuario = "#88888888";
            }
        }

        /// <summary>
        /// Método que inserta la categoria acertada mandanda por el shell a través de recorrer la lista de categorias del 
        /// usuarioLocal, y cuando la encuentre, hará un set a True al booleano EstaAcertada de la clase Categoria y igualara la imagen
        /// mostrada a la imagen correspondiente
        /// Precondiciones: Ninguna
        /// Postcondiciones: Ninguna
        /// </summary>
        private void asignarCategoriaAcertada()
        {
            for(int i=0; i<listaCategoriasLocal.Count; i++)
            {
                if (categoriaAcertada.Nombre.Contains(listaCategoriasLocal[i].Nombre));
                {
                    listaCategoriasLocal[i] = categoriaAcertada;
                }
            }
            NotifyPropertyChanged(nameof(ListaCategoriasLocal));
        }

        private void comprobarVictoria()
        {
            int cantidadAcertadas = 0;
            foreach (clsCategoriasMaui categoria in listaCategoriasLocal)
            {
                if (categoria.EstaAcertada)
                {
                    cantidadAcertadas++;
                }
            }
            if (cantidadAcertadas == listaCategoriasLocal.Count)
            {
                //notificarVictoriaSignalR
                //notificarVictoriaEnVista
            }


        }



        private clsPreguntas generarPregunta()
        {
            clsPreguntas preguntaElegida = new clsPreguntas();


            if (CategoriaDelaRuleta>=0 && CategoriaDelaRuleta <= 8)
            {
                preguntaElegida= devolverPreguntaPorCategoria(listadoPreguntashistory);
            }
            else if(CategoriaDelaRuleta >= 9 && CategoriaDelaRuleta <= 17)
            {
                preguntaElegida= devolverPreguntaPorCategoria(listadoPreguntasScience);
            }
            else if(CategoriaDelaRuleta >= 18 && CategoriaDelaRuleta <= 26)
            {
                preguntaElegida= devolverPreguntaPorCategoria(listadoPreguntasFood);
            }
            else if(CategoriaDelaRuleta >= 27 && CategoriaDelaRuleta <= 35)
            {
                preguntaElegida = devolverPreguntaPorCategoria(listadoPreguntasMusic);
            }
            else if(CategoriaDelaRuleta >= 36 && CategoriaDelaRuleta <= 44)
            {
                preguntaElegida = devolverPreguntaPorCategoria(listadoPreguntasFilms);
            }


            return preguntaElegida;
        }

        private static clsPreguntas devolverPreguntaPorCategoria(List<clsPreguntas> listadoPreguntas) {

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

        #endregion
    }
}
