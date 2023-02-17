using Microsoft.AspNetCore.SignalR;


namespace SignalR_Proyecto.Hubs
{
    public class salaEsperaHub :Hub
    {

        public async Task enviarBool(string hayRival)
        {

            await Clients.Others.SendAsync("recibirBool", hayRival);

        }


    }
}
