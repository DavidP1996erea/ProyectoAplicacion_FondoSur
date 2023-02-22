using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using PregunFondoSur.ViewModels.Utilidades;
using System.Text;
using System.Threading.Tasks;
using PregunFondoSur.models;
using System.Windows.Input;

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

        private void acertarCategoria(String nombreCategoria)
        {
            clsCategoriasMaui categoriaResultado = obtenerCategoriaPorNombre(nombreCategoria);
            categoriaResultado.ImagenMostrada = categoriaResultado.ImagenAcertada;
            categoriaResultado.EstaAcertada = true;

            volverAEleccionCategoria(categoriaResultado);
        }

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

        private async void volverAEleccionCategoria(clsCategoriasMaui categoriaAcertada)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "categoria", categoriaAcertada }
            };
            await Shell.Current.GoToAsync("..", navigationParameter);
        }


        private void obtenerListadoRespuestas(List<String> listadoRespuestas, clsPreguntas pregunta)
        {
            listadoRespuestas.Add(pregunta.incorrectAnswers[0]);
            listadoRespuestas.Add(pregunta.incorrectAnswers[1]);
            listadoRespuestas.Add(pregunta.incorrectAnswers[2]);
            listadoRespuestas.Add(pregunta.correctAnswer);

            ListadoRespuestas = new List<string>(randomizar(listadoRespuestas));
            

        }

        public static List<string> randomizar(List<string> list)
        {
            List<string> randomizedList = new List<string>();
            Random rnd = new Random();
            while (list.Count > 0)
            {
                int index = rnd.Next(0, list.Count); //pick a random item from the master list
                randomizedList.Add(list[index]); //place it at the end of the randomized list
                list.RemoveAt(index); //remove to avoid duplicates
            }
            return randomizedList;
        }

        private void pulsarBotonCommand_Executed(string parametro)
        {
            int posicion = int.Parse(parametro);
            respuestaSeleccionada = listadoRespuestas[posicion];
            comprobarSiCorrecto();
        }
        private void comprobarSiCorrecto()
        {
            if (respuestaSeleccionada == pregunta.correctAnswer)
            {
                Console.WriteLine("acertada");
            }
            else
            {
                Console.WriteLine("Fallado");
            }
        }
        #endregion

        #region Metodos SignalR



        #endregion
    }
}

