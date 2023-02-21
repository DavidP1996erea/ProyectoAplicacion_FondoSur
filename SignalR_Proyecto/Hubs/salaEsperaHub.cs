using Microsoft.AspNetCore.SignalR;
using SignalR_Proyecto.Models;

namespace SignalR_Proyecto.Hubs
{
    public class salaEsperaHub :Hub
    {

        
        public async Task enviarBool(string hayRival)
        {
            clsUsuariosPartida.ListadoUsuariosPartida.Clear();
            await Clients.Others.SendAsync("recibirBool", hayRival);

        }


    }
}
