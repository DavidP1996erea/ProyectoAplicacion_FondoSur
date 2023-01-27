using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class clsPreguntas
    {

        #region Atributos
        String pregunta { get; set; }
        List <clsRespuestas> listadoRespuestas { get; set; }

        #endregion

        #region Constructor por defecto
        public clsPreguntas()
        {

        }
        #endregion


        #region Constructor con parámetros
        public clsPreguntas(string pregunta, List<clsRespuestas> listadoRespuestas)
        {
            this.pregunta = pregunta;
            this.listadoRespuestas = listadoRespuestas;
        }
        #endregion

    }
}
