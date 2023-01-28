using Entidades;
using PregunFondoSur.ViewModels.Utilidades;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregunFondoSur.ViewModels
{
    public class clsPruebaRuben : clsVMBase
    {
        public List<clsPreguntas> listadoPreguntasFilms { get; set; }
        public List<clsPreguntas> listadoPreguntasMusic { get; set; }
        public List<clsPreguntas> listadoPreguntashistory { get; set; }
        public List<clsPreguntas> listadoPreguntasFood { get; set; }
        public List<clsPreguntas> listadoPreguntasScience { get; set; }
        public DelegateCommand btnMostrarCommand { get; set; }


        public clsPruebaRuben()
        {
            btnMostrarCommand = new DelegateCommand(mostrarCommand_Executed);
        }

        private void mostrarCommand_Executed()
        {
            obtenerListadoPreguntas();
        }

        private async Task obtenerListadoPreguntas()
        {
            listadoPreguntasFilms = await clsObtenerListadoPreguntasPorCategoria.obtenerListadoPreguntasFilmDAL();
            listadoPreguntasMusic = await clsObtenerListadoPreguntasPorCategoria.obtenerListadoPreguntasMusicDAL();
            listadoPreguntashistory = await clsObtenerListadoPreguntasPorCategoria.obtenerListadoPreguntasHistoryDAL();
            listadoPreguntasFood = await clsObtenerListadoPreguntasPorCategoria.obtenerListadoPreguntasFoodDAL();
            listadoPreguntasScience = await clsObtenerListadoPreguntasPorCategoria.obtenerListadoPreguntasScienceDAL();
            NotifyPropertyChanged(nameof(listadoPreguntasFilms));
        }



    }
}
