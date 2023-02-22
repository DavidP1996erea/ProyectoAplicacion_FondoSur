using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using PregunFondoSur.ViewModels.Utilidades;
using System.Text;
using System.Threading.Tasks;
using PregunFondoSur.models;

namespace PregunFondoSur.ViewModels
{
    [QueryProperty(nameof(Pregunta), "pregunta")]
    public class clsPreguntasVM : clsVMBase
    {
        #region Atributos
        private clsPreguntas pregunta;
        private int tiempo;
       
        #endregion

        #region Propiedades
        public clsPreguntas Pregunta { get { return pregunta; } set { pregunta = value; NotifyPropertyChanged(); } }
        public int Tempo { get { return tiempo; } set { tiempo = value; } }
      
        #endregion

        #region Constructores
        public clsPreguntasVM() {
            tiempo = 60;
     
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

        private clsCategoriasMaui obtenerCategoriaPorNombre(String nombreCategoria) {
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

        private async void volverAEleccionCategoria(clsCategoriasMaui categoriaAcertada) {
            var navigationParameter = new Dictionary<string, object>
            {
                { "categoria", categoriaAcertada }
            };
            await Shell.Current.GoToAsync("..", navigationParameter);
        }
        #endregion

        #region Metodos SignalR



        #endregion
    }
}

