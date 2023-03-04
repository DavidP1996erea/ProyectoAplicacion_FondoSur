using Microsoft.AspNetCore.SignalR;
using Models;
using SignalR_Proyecto.Models;

namespace SignalR_Proyecto.Hubs
{
    public class salasHub : Hub
    {

        /// <summary>
        /// Metodo que recibe el nombre de la sala creada, y lo añade al listado de salas
        /// y envia el nombre de sala
        /// </summary>
        /// <param name="nombreSala"></param>
        /// <returns></returns>
        public async Task crearSala(string nombreSala)
        {
            List<String> list = clsListadoSalas.ListadoSalas;
            
            list.Add(nombreSala);

            await Clients.Others.SendAsync("recibirCrearSala", nombreSala);
         }

        /// <summary>
        /// Metodo que recibe un listado de salas y le asigna a ese listado el listado estatico
        /// que tenemos creado en el signalR, luego devolvemos el listado que envio el usuario
        /// pero ya cargado.
        /// </summary>
        /// <param name="listadoSalas"></param>
        /// <returns></returns>
        public async Task enviarListadoSalas(List<string> listadoSalas)
        {
            listadoSalas = new List<string>(clsListadoSalas.ListadoSalas);

            await Clients.All.SendAsync("recibirListadoSalas", listadoSalas);
        }

    }
}
