using Entidades;
using Microsoft.AspNetCore.SignalR;
using Models;
using SignalR_Proyecto.Models;

namespace SignalR_Proyecto.Hubs
{

    public class eleccionCategoriasHub : Hub
    {

      

        public async Task enviarUsuario(clsUsuario datosUsuario)
        {
            datosUsuario.tuTurno = false;
          

            if (clsUsuariosPartida.ListadoUsuariosPartida.Count==0)
            {
               datosUsuario.tuTurno=true;
               clsUsuariosPartida.ListadoUsuariosPartida.Add(datosUsuario);
            }


            await Clients.All.SendAsync("recibirUsuario", datosUsuario);

        }

        public async Task enviarListadoCategorias(List<clsCategorias> listadoCategorias)
        {
            await Clients.Others.SendAsync("recibirListadoCategorias", listadoCategorias);
        }

        public async Task enviarCambiarValorTurno(String miTurno)
        {
            
            await Clients.Others.SendAsync("recibirCambiarTurno", miTurno);
        }


        public async Task enviarBoolFinPartida(string partidaAcabada)
        {
            
            await Clients.Others.SendAsync("recibirBoolFinPartida", partidaAcabada);

        }

    }
}
