using Entidades;
using Microsoft.AspNetCore.SignalR;
using Models;

namespace SignalR_Proyecto.Hubs
{
    public class eleccionCategoriasHub : Hub
    {
        public async Task enviarUsuario(clsUsuario datosUsuario )
        {

            await Clients.Others.SendAsync("recibirUsuario", datosUsuario);

        }



        public async Task enviarListadoCategorias(List<clsCategorias> listadoCategorias)
        {

            await Clients.Others.SendAsync("recibirListadoCategorias", listadoCategorias);

        }

    }
}
