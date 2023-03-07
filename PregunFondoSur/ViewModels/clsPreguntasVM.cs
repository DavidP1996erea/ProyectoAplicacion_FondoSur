using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using PregunFondoSur.ViewModels.Utilidades;
using System.Text;
using System.Threading.Tasks;
using PregunFondoSur.models;
using System.Windows.Input;
using PregunFondoSur.Vistas;

namespace PregunFondoSur.ViewModels
{
    [QueryProperty(nameof(Pregunta), "pregunta")]
    public class clsPreguntasVM : clsVMBase
    {
        #region Atributos
        private clsPreguntas pregunta;
        private List<String> listadoRespuestas;
        private String respuestaSeleccionada;
        private DelegateCommand<string> pulsarBotonCommand;
        private int idBoton;
        private int tiempo;

        #endregion

        #region Propiedades
        public clsPreguntas Pregunta { get { return pregunta; } set { pregunta = value;
                obtenerListadoRespuestas(listadoRespuestas, pregunta); NotifyPropertyChanged(); } }

        public List<String> ListadoRespuestas { get { return listadoRespuestas; } set { listadoRespuestas = value; NotifyPropertyChanged(); }  }
        public int IdBoton { 
            get { return idBoton; } 
            set { idBoton = value; }
        }

        public DelegateCommand<string> PulsarBotonCommand
        {
            get { return pulsarBotonCommand; }
            set { pulsarBotonCommand = value; }
        }

        public int Tempo { get { return tiempo; } set { tiempo = value; } }

        #endregion

        #region Constructores
        public clsPreguntasVM()
        {
            tiempo = 60;
            listadoRespuestas = new List<String>();
            pulsarBotonCommand = new DelegateCommand<string>(pulsarBotonCommand_Executed);
        }



        #endregion

        #region Metodos
        /// <summary>
        /// Metodo que recibe el nombre de categoria y si ha sido acertado
        /// Este metodo llama al metodo de obtenerCategoriaPorNombre y 
        /// si la categoria ha sido acertada, se le pondra la imagen acertada 
        /// y estaAcertada a true
        /// </summary>
        /// <param name="nombreCategoria"></param>
        /// <param name="acertado"></param>
        private void acertarCategoria(String nombreCategoria, bool acertado)
        {

            clsCategoriasMaui categoriaResultado = obtenerCategoriaPorNombre(nombreCategoria);
            if (acertado)
            {
                categoriaResultado.ImagenMostrada = categoriaResultado.ImagenAcertada;
                categoriaResultado.EstaAcertada = true;
            }
            

            volverAEleccionCategoria(categoriaResultado);
        }
        /// <summary>
        /// Metodo que recibe un nombre y recorre un listado de categorias
        /// y cuando tienen el mismo nombre returna la categoria con ese nombre.
        /// </summary>
        /// <param name="nombreCategoria"></param>
        /// <returns></returns>
        private clsCategoriasMaui obtenerCategoriaPorNombre(String nombreCategoria)
        {
            clsCategoriasMaui categoriaAcertada = new clsCategoriasMaui();
            List<clsCategoriasMaui> categorias = clsObtenerListadoCategorias.obtenerListadoCompletoCategorias();
            foreach (clsCategoriasMaui categoria in categorias)
            {
                if (categoria.Nombre.Contains(nombreCategoria))
                {


                    categoriaAcertada = categoria;
                }
            }
            return categoriaAcertada;
        }
        /// <summary>
        /// Metodo que te envia de vuelta a la pagina de eleccionCategoria 
        /// returnandote la categoria.
        /// </summary>
        /// <param name="categoriaAcertada"></param>
        private async void volverAEleccionCategoria(clsCategoriasMaui categoriaAcertada)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "categoria", categoriaAcertada }
            };
            await Shell.Current.GoToAsync("..", navigationParameter);
        }

        /// <summary>
        /// Metodo que áñade a un listado de strings las 3 respuestas incorrectas 
        /// y la respuesta correcta y llama al metodo de randomizar ¡
        /// </summary>
        /// <param name="listadoRespuestas"></param>
        /// <param name="pregunta"></param>
        private void obtenerListadoRespuestas(List<String> listadoRespuestas, clsPreguntas pregunta)
        {
            listadoRespuestas.Add(pregunta.incorrectAnswers[0]);
            listadoRespuestas.Add(pregunta.incorrectAnswers[1]);
            listadoRespuestas.Add(pregunta.incorrectAnswers[2]);
            listadoRespuestas.Add(pregunta.correctAnswer);

            ListadoRespuestas = new List<string>(randomizar(listadoRespuestas));
            

        }
        /// <summary>
        /// Metodo que randomiza un listado de string.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<string> randomizar(List<string> list)
        {
            List<string> randomizedList = new List<string>();
            Random rnd = new Random();
            while (list.Count > 0)
            {
                int index = rnd.Next(0, list.Count); 
                randomizedList.Add(list[index]); 
                list.RemoveAt(index); 
            }
            return randomizedList;
        }
        /// <summary>
        /// Metodo que recibe un parametro que son los numeros
        /// identificadores que tiene cada boton de la vista y 
        /// asi se comprueba si la respuesta seleccionada es la correcta.
        /// </summary>
        /// <param name="parametro"></param>
        private void pulsarBotonCommand_Executed(string parametro)
        {
            int posicion = int.Parse(parametro);
            respuestaSeleccionada = listadoRespuestas[posicion];
            comprobarSiCorrecto();
        }
        /// <summary>
        /// Metodo que segun si la respuesta seleccionada esta correcta
        ///  envia un true y en el caso contrario un false
        /// </summary>
        /// <returns></returns>
        private async Task comprobarSiCorrecto()
        {
            if (respuestaSeleccionada == pregunta.correctAnswer)
            {
                acertarCategoria(pregunta.category, true);
            }
            else
            {
                acertarCategoria(pregunta.category, false);
            }
        }
        #endregion


    }
}

