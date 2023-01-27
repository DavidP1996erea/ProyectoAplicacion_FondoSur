using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class clsRespuestas
    {
        #region Atributos
        String respuesta { get; set; }
        bool esCorrecta { get; set; }
        #endregion


        #region Constructor con parámetros
        public clsRespuestas(string respuesta, bool esCorrecta)
        {
            this.respuesta = respuesta;
            this.esCorrecta = esCorrecta;
        }
        #endregion

        #region Constructor por defecto
        public clsRespuestas()
        {

        }
        #endregion
    }
}
