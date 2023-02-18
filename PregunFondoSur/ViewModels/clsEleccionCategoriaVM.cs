﻿using Entidades;
using Microsoft.AspNetCore.SignalR.Client;
using PregunFondoSur.models;
using PregunFondoSur.ViewModels.Utilidades;
using Services;
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
        private clsUsuario usuarioRival;
        private List<clsCategorias> listaCategoriasLocal;
        private List<clsCategorias> listaCategoriasRival;
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
        #endregion

        #region Propiedades

        //TODO ARREGLAR
        public clsCategorias CategoriaAcertada
        {
            get { return CategoriaAcertada; }
            set
            {
                CategoriaAcertada = value;
                asignarCategoriaAcertada();
            }
        }
        public clsUsuario UsuarioLocal { get; set; }

        public clsUsuario UsuarioRival
        {
            get { return usuarioRival; }
            set { usuarioRival = value; }
        }
        public List<clsCategorias> ListaCategoriasLocal
        {
            get { return listaCategoriasLocal; }
            set { listaCategoriasLocal = value;
                comprobarVictoria();
            }
        }
        public List<clsCategorias> ListaCategoriasRival
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
                girarRuletaCommand.RaiseCanExecuteChanged();
                establecerColorFondo();
            }
        }

        public String ColorFondoUsuario
        {
            get { return colorFondoUsuario; }
            set { colorFondoUsuario = value; }
        }
        #endregion

        #region Constructores
        public clsEleccionCategoriaVM()
        {
            obtenerListados();
            listaCategoriasLocal = clsObtenerListadoCategorias.obtenerListadoCompletoCategorias();
            listaCategoriasRival = clsObtenerListadoCategorias.obtenerListadoCompletoCategorias();
            girarRuletaCommand = new DelegateCommand(girarRuletaCommand_Executed, girarRuletaCommand_CanExecuted);

            // Se crea la conexión con el servidor
            miConexion = new HubConnectionBuilder().WithUrl("https://proyectofondosur.azurewebsites.net/eleccionCategoriasHub").Build();
            UsuarioLocal = new clsUsuario();
            UsuarioLocal.userName = "Ruben londres";


            recibirUsuario();
            enviarUsuario();
        }

        #endregion


        #region Métodos SignalR


        private async Task enviarUsuario()
        {
            await miConexion.InvokeCoreAsync("enviarUsuario", args: new[] { UsuarioLocal });
        }

        private async Task recibirUsuario()
        {

            int cont = 1;

            miConexion.On<clsUsuario>("recibirUsuario", async (datosUsuario) => {


                if (cont > 0)
                {
                    cont--;           
                    UsuarioRival = datosUsuario;
                    await enviarUsuario();
                }
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool girarRuletaCommand_CanExecuted()
        {
            bool pulsable = false;
            if (tuTurno)
            {
                if (!estaGirando)
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


            //TODO Implementar Chuleta
            var navigationParameter = new Dictionary<string, object>
            {
                { "preguntaEnviar", preguntaEnviar }
            };
            await Shell.Current.GoToAsync($"PaginaPregunta", navigationParameter);
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
            foreach (clsCategorias categoria in listaCategoriasLocal)
            {
                if (CategoriaAcertada.Nombre == categoria.Nombre)
                {
                    categoria.EstaAcertada = true;
                    categoria.ImagenMostrada = categoria.ImagenAcertada;
                }
            }
        }

        private void comprobarVictoria()
        {
            int cantidadAcertadas = 0;
            foreach (clsCategorias categoria in listaCategoriasLocal)
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

        #endregion
    }
}
