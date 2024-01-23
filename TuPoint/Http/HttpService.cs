using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TuPoint.Http
{
    public class HttpService
    {
        private readonly string baseUrl = "https://tupoint.com/apk/mobile/"; // Reemplaza con tu URL

        private readonly HttpClient _httpClient;

        public HttpService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Manejar el caso de respuesta no exitosa
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                return null;
            }
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Construir los parámetros de la solicitud
                    var parameters = new Dictionary<string, string>
                {
                    { "user", email },
                    { "contra", password }
                };

                    // Codificar los parámetros en formato de URL
                    var encodedContent = new FormUrlEncodedContent(parameters);

                    // Realizar la solicitud HTTP POST
                    HttpResponseMessage response = await client.PostAsync(baseUrl+"login.php", encodedContent);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leer y devolver la respuesta del servidor
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        // Manejar el caso de respuesta no exitosa
                        return "error";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                return "Error: " + ex.Message;
            }
        }
    }
}
