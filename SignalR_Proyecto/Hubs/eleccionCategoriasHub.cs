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

            await Groups.AddToGroupAsync(Context.ConnectionId, datosUsuario.nombreSala);
            await Clients.Group(datosUsuario.nombreSala).SendAsync("recibirUsuario", datosUsuario);

        }

        public async Task enviarListadoCategorias(List<clsCategorias> listadoCategorias, String nombreSala)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, nombreSala);
            await Clients.OthersInGroup(nombreSala).SendAsync("recibirListadoCategorias", listadoCategorias);
        }

        public async Task enviarCambiarValorTurno(String miTurno, String nombreSala)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, nombreSala);
            await Clients.OthersInGroup(nombreSala).SendAsync("recibirCambiarTurno", miTurno);
        }


    }
}
