using PregunFondoSur.ViewModels.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregunFondoSur.ViewModels
{
    public class clsResultadosVM : clsVMBase
    {
        private bool hasGanado;
        private string textoMostrar;

        #region Propiedades
        public string TextoMostrar
        {
            get { return textoMostrar; }
            set { textoMostrar = value; }
        }
        #endregion

        #region Constructores
        public clsResultadosVM()
        {

        }

        public clsResultadosVM(bool hasGanado, string textoMostrar)
        {
            this.hasGanado = hasGanado;
            this.textoMostrar = textoMostrar;
        }
        #endregion 


        private void MostrarResultados()
        {
            if (!hasGanado)
            {
                textoMostrar = "FELICIDADES HAS GANADO";
            }
            else
            {
                textoMostrar = "HAS PERDIDO";
            }
            NotifyPropertyChanged(nameof(TextoMostrar));
        }
    }
}
