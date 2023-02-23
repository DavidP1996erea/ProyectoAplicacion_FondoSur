using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregunFondoSur.models
{
    public class clsDatosResultadoPartida
    {
        public clsUsuario usuarioLocal;
        public clsUsuario usuarioRival;
        public List<clsCategoriasMaui> categoriasUsuarioLocal;
        public List<clsCategoriasMaui> categoriasUsuarioRival;


        public clsDatosResultadoPartida() { }
        public clsDatosResultadoPartida(clsUsuario usuarioLocal, clsUsuario usuarioRival, List<clsCategoriasMaui> categoriasUsuarioLocal, List<clsCategoriasMaui> categoriasUsuarioRival)
        {
            this.usuarioLocal = usuarioLocal;
            this.usuarioRival = usuarioRival;
            this.categoriasUsuarioLocal = categoriasUsuarioLocal;
            this.categoriasUsuarioRival = categoriasUsuarioRival;
        }
    }
}
