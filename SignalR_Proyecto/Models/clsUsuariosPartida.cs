using Entidades;

namespace SignalR_Proyecto.Models
{
    public static class clsUsuariosPartida
    {
        private static List<clsUsuario> listadoUsuariosPartida = new List<clsUsuario>(); 


        public static List<clsUsuario> ListadoUsuariosPartida
        {
            get { return listadoUsuariosPartida;  }
            set { listadoUsuariosPartida = value; }
        }




      


    }



}
