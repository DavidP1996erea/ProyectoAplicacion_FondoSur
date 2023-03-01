using Microsoft.AspNetCore.SignalR;
using Models;
using SignalR_Proyecto.Models;

namespace SignalR_Proyecto.Hubs
{
    public class salasHub : Hub
    {


        public async Task crearSala(string nombreSala)
        {         
            clsListadoSalas.ListadoSalas.Add(nombreSala);

            await Clients.Others.SendAsync("recibirCrearSala", nombreSala);
         }


        public async Task enviarListadoSalas(List<string> listadoSalas)
        {
            listadoSalas = new List<string>(clsListadoSalas.ListadoSalas);
            listadoSalas.Add("sala1");

            await Clients.Others.SendAsync("recibirListadoSalas", listadoSalas);
        }

    }
}
