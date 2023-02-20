using Entidades;
using Microsoft.AspNetCore.SignalR;
using Models;
using SignalR_Proyecto.Models;

namespace SignalR_Proyecto.Hubs
{
    public class eleccionCategoriasHub : Hub
    {
        public async Task enviarUsuario(clsUsuario datosUsuario )
        {
            
            if (clsUsuariosPartida.listadoUsuariosPartida[0]==null) {
                clsUsuariosPartida.listadoUsuariosPartida.Add(datosUsuario);
            }
            else
            {
                datosUsuario.tuTurno = false;
            }

            await Clients.Others.SendAsync("recibirUsuario", datosUsuario);

        }



        public async Task enviarListadoCategorias(List<clsCategorias> listadoCategorias)
        {

            await Clients.Others.SendAsync("recibirListadoCategorias", listadoCategorias);

        }

    }
}
