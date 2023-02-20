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
            datosUsuario.tuTurno = true;


            if (clsUsuariosPartida.listadoUsuariosPartida[0]==null) {
               datosUsuario.tuTurno=false;
               clsUsuariosPartida.listadoUsuariosPartida.Add(datosUsuario);
            }
            
            await Clients.Others.SendAsync("recibirUsuario", datosUsuario);

        }

        public async Task enviarListadoCategorias(List<clsCategorias> listadoCategorias)
        {
            await Clients.Others.SendAsync("recibirListadoCategorias", listadoCategorias);
        }

        public async Task cambiarValorTurno(String miTurno)
        {
            bool turno = false;
            if (miTurno == "true") {
                turno = true;
            }
            await Clients.Others.SendAsync("cambiarTurno", turno);
        }

    }
}
