using Microsoft.Extensions.Configuration;
using MvcPrimeraPracticaAzure.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcPrimeraPracticaAzure.Services
{
    public class ServicePersonajes
    {

        private MediaTypeWithQualityHeaderValue header;
        private string ApiUrl;

        public ServicePersonajes(IConfiguration configuration)
        {
            this.ApiUrl = configuration.GetValue<string>("ApiUrls:ApiPersonajes"); 
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }
        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "api/personajes/getpersonajes";
            List<Personaje> data = await this.CallApiAsync<List<Personaje>>(request);
            return data;
        }
        public async Task<Personaje> FindPersonajeAsync(int idPersonaje)
        {
            string request = "api/personajes/findpersonaje/" + idPersonaje;
            Personaje data = await this.CallApiAsync<Personaje>(request);
            return data;
        }


        public async Task InsertPersonajeAsync(Personaje peli)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes/insertarpersonaje";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                string json = JsonConvert.SerializeObject(peli);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task UpdatePersonajeAsync(Personaje peli)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes/updatepersonaje";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                string json = JsonConvert.SerializeObject(peli);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

        public async Task DeletePersonajeAsync(int idPersonaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes/deletepersonaje/" + idPersonaje;
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();

                //NO ENVIAMOS NADA PORQUE NO RECIBE NADA NI DEVUELVE 
                //NADA 
                HttpResponseMessage response = await client.DeleteAsync(request);
                //PODRIAMOS DEVOLVER QUE ES LO QUE HA SUCEDIDO 
                //POR EJEMPLO, DEVOLVIENDO STATUS CODE 
                //return response.StatusCode; 
            }
        }
        
        
        public async Task<List<string>> GetSeriesAsync()
        {
            string request = "api/personajes/series";
            List<string> data = await this.CallApiAsync<List<string>>(request);
            return data;
        }

        public async Task<List<Personaje>> GetPersonajesSerieAsync(string serie)
        {
            string request = "api/personajes/getpersonajesserie/" + serie + "";
            List<Personaje> data = await this.CallApiAsync<List<Personaje>>(request);
            return data;
        }


    }
}
