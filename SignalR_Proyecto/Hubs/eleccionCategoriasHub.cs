using Entidades;
using Microsoft.AspNetCore.SignalR;


namespace SignalR_Proyecto.Hubs
{
    public class eleccionCategoriasHub : Hub
    {
        public async Task enviarUsuario(clsUsuario datosUsuario )
        {

            await Clients.Others.SendAsync("recibirUsuario", datosUsuario);

        }



        public async Task enviarListadoCategorias(List<clsCategorias> datosUsuario)
        {

            await Clients.Others.SendAsync("recibirListadoCategorias", datosUsuario);

        }

    }
}
