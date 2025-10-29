using Proyecto2025.Servicio.ChatMemberServicioHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Proyecto2025.Servicio.ChatServicioHttp
{
    public class ChatServicio : IChatServicio
    {
        private readonly HttpClient http;
        public ChatServicio(HttpClient Http)
        {
            this.http = Http;
        }

        public async Task<ChatRespuesta<T>> GetEsAsync<T>(string url)
        {
            var Response = await http.GetAsync(url);
            if (Response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerializar<T>(Response);
                return new ChatRespuesta<T>(respuesta, false, Response);
            }
            else
            {
                return new ChatRespuesta<T>(default, true, Response);
            }

        }

        public async Task<ChatRespuesta<Tresp>> Post<T, Tresp>(string url, T enviar)
        {
            var JsonAEnviar = JsonSerializer.Serialize(enviar);
            var contenido = new StringContent(JsonAEnviar,
                                              System.Text.Encoding.UTF8,
                                              "application/json");
            var response = await http.PostAsync(url, contenido);
            if (response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerializar<Tresp>(response);
                return new ChatRespuesta<Tresp>(respuesta, false, response);
            }
            else
            {
                return new ChatRespuesta<Tresp>(default, true, response);
            }
        }

        public async Task<ChatRespuesta<Tresp>> Put<T, Tresp>(string url, T enviar)
        {
            var JsonAEnviar = JsonSerializer.Serialize(enviar);
            var contenido = new StringContent(JsonAEnviar,
                                              System.Text.Encoding.UTF8,
                                              "application/json");
            var response = await http.PutAsync(url, contenido);
            if (response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerializar<Tresp>(response);
                return new ChatRespuesta<Tresp>(respuesta, false, response);
            }
            else
            {
                return new ChatRespuesta<Tresp>(default, true, response);
            }
        }

        public async Task<ChatRespuesta<object>> Delete(string url)
        {
            {
                var response = await http.DeleteAsync(url);


                return new ChatRespuesta<object>(null,
                                                       !response.IsSuccessStatusCode,
                                                        response);
            }
        }

        private async Task<T?> DesSerializar<T>(HttpResponseMessage response)
        {
            var respuestaStri = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(respuestaStri,
              new JsonSerializerOptions
              {
                  PropertyNameCaseInsensitive = true
              });
        }
    }
}
