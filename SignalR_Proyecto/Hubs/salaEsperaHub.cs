using Entidades;
using Microsoft.AspNetCore.SignalR;
using Models;
using SignalR_Proyecto.Models;

namespace SignalR_Proyecto.Hubs
{
    public class salaEsperaHub :Hub
    {

        /// <summary>
        /// Metodo que recibe un usuario, borra el listado de usuarios y luego envia a todos los miembros del grupo 
        /// el usuario.
        /// </summary>
        /// <param name="usuarioEspera"></param>
        /// <returns></returns>
        public async Task enviarBool(clsUsuario usuarioEspera)
        {
            clsUsuariosPartida.ListadoUsuariosPartida.Clear();
           
            await Groups.AddToGroupAsync(Context.ConnectionId, usuarioEspera.nombreSala);
            await Clients.Group(usuarioEspera.nombreSala).SendAsync("recibirBool", usuarioEspera);

        }


    }
}
