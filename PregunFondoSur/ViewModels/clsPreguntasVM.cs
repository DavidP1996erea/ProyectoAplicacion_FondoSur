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
        private DelegateCommand pulsarBotonCommand;
        #endregion

        #region Propiedades
        public clsPreguntas Pregunta { get { return pregunta; } set { pregunta = value; } }
        public int Tempo { get { return tiempo; } set { tiempo = value; } }
        public DelegateCommand PulsarBotonCommand { get { return pulsarBotonCommand; } }
        #endregion

        #region Constructores
        public clsPreguntasVM() {
            tiempo = 60;
            pulsarBotonCommand = new DelegateCommand(pulsarBotonCommand_Execute, pulsarBotonCommand_CanExecute);
        }


        #endregion

        #region Metodos
        private bool pulsarBotonCommand_CanExecute()
        {
            return true;
        }

        private void pulsarBotonCommand_Execute()
        { 
           bool acertado=false;
            //TODO recoger la pregunta y validar si esta bien o mal
            clsCategorias categoriaAcertada=new clsCategorias();
            if (acertado) {
                String nombreCategoria = pregunta.category;
                List<clsCategorias> categorias = clsObtenerListadoCategorias.obtenerListadoCompletoCategorias();
                foreach (clsCategorias categoria in categorias)
                {
                    if (categoria.Nombre==nombreCategoria) {
                        categoriaAcertada = categoria;
                    }
                }
                volverAEleccionCategoria(categoriaAcertada);
            }


        }

        private async void volverAEleccionCategoria(clsCategorias categoriaAcertada) {
            var navigationParameter = new Dictionary<string, object>
            {
                { "categoria", categoriaAcertada }
            };
            await Shell.Current.GoToAsync("PaginaEleccionCategoria", navigationParameter);
        }
        #endregion

        #region Metodos SignalR
        #endregion
    }
}

