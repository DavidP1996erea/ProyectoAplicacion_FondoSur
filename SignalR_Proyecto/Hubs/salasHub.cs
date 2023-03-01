using Microsoft.AspNetCore.SignalR;
using Models;
using SignalR_Proyecto.Models;

namespace SignalR_Proyecto.Hubs
{
    public class salasHub : Hub
    {


        public async Task crearSala(string nombreSala)
        {
            List<String> list = clsListadoSalas.ListadoSalas;
            
            list.Add(nombreSala);

            await Clients.Others.SendAsync("recibirCrearSala", nombreSala);
         }


        public async Task enviarListadoSalas(List<string> listadoSalas)
        {
            listadoSalas = new List<string>(clsListadoSalas.ListadoSalas);

            await Clients.All.SendAsync("recibirListadoSalas", listadoSalas);
        }

    }
}
