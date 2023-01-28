﻿using Entidades;
using Newtonsoft.Json;
using Services.Conexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class clsObtenerListadoPreguntasPorCategoria
    {
        public static async Task<List<clsPreguntas>> obtenerListadoPreguntasFilmDAL()
        {

            string miCadenaUrl = clsURL.obtenerConexion();

            Uri miUri = new Uri($"{miCadenaUrl}questions?categories=film_and_tv&limit=40");

            List<clsPreguntas> listadoPreguntasFilm = new List<clsPreguntas>();

            HttpClient mihttpClient;

            HttpResponseMessage miCodigoRespuesta;

            string textoJsonRespuesta;


            mihttpClient = new HttpClient();
            try

            {

                miCodigoRespuesta = await mihttpClient.GetAsync(miUri);

                if (miCodigoRespuesta.IsSuccessStatusCode)

                {

                    textoJsonRespuesta = await mihttpClient.GetStringAsync(miUri);

                    mihttpClient.Dispose();

                    listadoPreguntasFilm = JsonConvert.DeserializeObject<List<clsPreguntas>>(textoJsonRespuesta);

                }

            }

            catch (Exception ex)

            {

                throw ex;

            }

            return listadoPreguntasFilm;

        }
        public static async Task<List<clsPreguntas>> obtenerListadoPreguntasFoodDAL()
        {

            string miCadenaUrl = clsURL.obtenerConexion();

            Uri miUri = new Uri($"{miCadenaUrl}questions?categories=food_and_drink&limit=40");

            List<clsPreguntas> listadoPreguntasFood = new List<clsPreguntas>();

            HttpClient mihttpClient;

            HttpResponseMessage miCodigoRespuesta;

            string textoJsonRespuesta;


            mihttpClient = new HttpClient();
            try

            {

                miCodigoRespuesta = await mihttpClient.GetAsync(miUri);

                if (miCodigoRespuesta.IsSuccessStatusCode)

                {

                    textoJsonRespuesta = await mihttpClient.GetStringAsync(miUri);

                    mihttpClient.Dispose();

                    listadoPreguntasFood = JsonConvert.DeserializeObject<List<clsPreguntas>>(textoJsonRespuesta);

                }

            }

            catch (Exception ex)

            {

                throw ex;

            }

            return listadoPreguntasFood;

        }
        public static async Task<List<clsPreguntas>> obtenerListadoPreguntasMusicDAL()
        {

            string miCadenaUrl = clsURL.obtenerConexion();

            Uri miUri = new Uri($"{miCadenaUrl}questions?categories=music&limit=40");

            List<clsPreguntas> listadoPreguntasMusic = new List<clsPreguntas>();

            HttpClient mihttpClient;

            HttpResponseMessage miCodigoRespuesta;

            string textoJsonRespuesta;


            mihttpClient = new HttpClient();
            try

            {

                miCodigoRespuesta = await mihttpClient.GetAsync(miUri);

                if (miCodigoRespuesta.IsSuccessStatusCode)

                {

                    textoJsonRespuesta = await mihttpClient.GetStringAsync(miUri);

                    mihttpClient.Dispose();

                    listadoPreguntasMusic = JsonConvert.DeserializeObject<List<clsPreguntas>>(textoJsonRespuesta);

                }

            }

            catch (Exception ex)

            {

                throw ex;

            }

            return listadoPreguntasMusic;

        }
        public static async Task<List<clsPreguntas>> obtenerListadoPreguntasHistoryDAL()
        {

            string miCadenaUrl = clsURL.obtenerConexion();

            Uri miUri = new Uri($"{miCadenaUrl}questions?categories=history&limit=40");

            List<clsPreguntas> listadoPreguntasHistory = new List<clsPreguntas>();

            HttpClient mihttpClient;

            HttpResponseMessage miCodigoRespuesta;

            string textoJsonRespuesta;


            mihttpClient = new HttpClient();
            try

            {

                miCodigoRespuesta = await mihttpClient.GetAsync(miUri);

                if (miCodigoRespuesta.IsSuccessStatusCode)

                {

                    textoJsonRespuesta = await mihttpClient.GetStringAsync(miUri);

                    mihttpClient.Dispose();

                    listadoPreguntasHistory = JsonConvert.DeserializeObject<List<clsPreguntas>>(textoJsonRespuesta);

                }

            }

            catch (Exception ex)

            {

                throw ex;

            }

            return listadoPreguntasHistory;

        }
        public static async Task<List<clsPreguntas>> obtenerListadoPreguntasScienceDAL()
        {

            string miCadenaUrl = clsURL.obtenerConexion();

            Uri miUri = new Uri($"{miCadenaUrl}questions?categories=science&limit=40");

            List<clsPreguntas> listadoPreguntasScience = new List<clsPreguntas>();

            HttpClient mihttpClient;

            HttpResponseMessage miCodigoRespuesta;

            string textoJsonRespuesta;


            mihttpClient = new HttpClient();
            try

            {

                miCodigoRespuesta = await mihttpClient.GetAsync(miUri);

                if (miCodigoRespuesta.IsSuccessStatusCode)

                {

                    textoJsonRespuesta = await mihttpClient.GetStringAsync(miUri);

                    mihttpClient.Dispose();

                    listadoPreguntasScience = JsonConvert.DeserializeObject<List<clsPreguntas>>(textoJsonRespuesta);

                }

            }

            catch (Exception ex)

            {

                throw ex;

            }

            return listadoPreguntasScience;

        }
    }
}
