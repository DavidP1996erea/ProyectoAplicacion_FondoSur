using Microsoft.AspNetCore.SignalR;
using Models;

namespace SignalR_Proyecto.Hubs
{
    public class salasHub : Hub
    {


        public async Task crearSala(string nombreSala)
        {
          
            clsListadoSalas.ListadoSalas.Add(nombreSala);


        }


    }
}
