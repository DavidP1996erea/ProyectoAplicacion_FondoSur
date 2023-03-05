using Entidades;
using Microsoft.AspNetCore.SignalR;
using Models;
using SignalR_Proyecto.Models;

namespace SignalR_Proyecto.Hubs
{

    public class eleccionCategoriasHub : Hub
    {


        /// <summary>
        /// metodo de signalR que recibe un usuario luego comprueba si la lista estatica
        /// que tenemos creada en el signalR esta vacia, si es asi, le asigna true del turno al jugador, y lo añade a la lista
        /// y asi cuando reciba al segundo usuario, no se le asignara el true, por lo que no sera su turno.
        /// Con este metodo enviamos los usuarios y tambien le asignamos si es su turno al empezar la partida.
        /// Precondiciones:
        /// Postcodiciones:
        /// </summary>
        /// <param name="datosUsuario"></param>
        /// <returns></returns>
        public async Task enviarUsuario(clsUsuario datosUsuario)
        {
            datosUsuario.tuTurno = false;
          

            if (clsUsuariosPartida.ListadoUsuariosPartida.Count==0)
            {
               datosUsuario.tuTurno=true;
               clsUsuariosPartida.ListadoUsuariosPartida.Add(datosUsuario);
            }
            clsListadoSalas.eliminarSala(datosUsuario.nombreSala);

            await Groups.AddToGroupAsync(Context.ConnectionId, datosUsuario.nombreSala);
            await Clients.Group(datosUsuario.nombreSala).SendAsync("recibirUsuario", datosUsuario);

        }
        /// <summary>
        /// Metodo que recibe un listado de categoria y el nombre de la sala, luego envia a 
        /// los otros miembros del grupo el listado de sala recibido.
        /// Precondiciones:
        /// Postcodiciones:
        /// </summary>
        /// <param name="listadoCategorias"></param>
        /// <param name="nombreSala"></param>
        /// <returns></returns>
        public async Task enviarListadoCategorias(List<clsCategorias> listadoCategorias, String nombreSala)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, nombreSala);
            await Clients.OthersInGroup(nombreSala).SendAsync("recibirListadoCategorias", listadoCategorias);
        }
        /// <summary>
        /// Metodo que recibe un string de turno y otro con el nombre de la sala, luego envia a los
        /// otros miembros del grupo el string de que es su turno.
        /// Precondiciones:
        /// Postcodiciones:
        /// </summary>
        /// <param name="miTurno"></param>
        /// <param name="nombreSala"></param>
        /// <returns></returns>
        public async Task enviarCambiarValorTurno(String miTurno, String nombreSala)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, nombreSala);
            await Clients.OthersInGroup(nombreSala).SendAsync("recibirCambiarTurno", miTurno);
        }


    }
}
