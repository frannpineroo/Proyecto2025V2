using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Proyecto2025.Servicio.ChatMemberServicioHttp
{
    public class ChatMemberServicio : IChatMemberServicio
    {
        private readonly HttpClient http;
        public ChatMemberServicio(HttpClient Http)
        {
            this.http = Http;
        }
        public async Task<ChatMemberRespuesta<T>> Get<T>(string url)
        {
            var Response = await http.GetAsync(url);
            if (Response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerializar<T>(Response);
                return new ChatMemberRespuesta<T>(respuesta, false, Response);
            }
            else
            {
                return new ChatMemberRespuesta<T>(default, true, Response);
            }

        }

        public async Task<ChatMemberRespuesta<Tresp>> Put<T, Tresp>(string url, T enviar)
        {
            var JsonAEnviar = JsonSerializer.Serialize(enviar);
            var contenido = new StringContent(JsonAEnviar,
                                              System.Text.Encoding.UTF8,
                                              "application/json");
            var response = await http.PutAsync(url, contenido);
            if (response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerializar<Tresp>(response);
                return new ChatMemberRespuesta<Tresp>(respuesta, false, response);
            }
            else
            {
                return new ChatMemberRespuesta<Tresp>(default, true, response);
            }
        }

        public async Task<ChatMemberRespuesta<Tresp>> Post<T, Tresp>(string url, T enviar)
        {
            var JsonAEnviar = JsonSerializer.Serialize(enviar);
            var contenido = new StringContent(JsonAEnviar,
                                              System.Text.Encoding.UTF8,
                                              "application/json");
            var response = await http.PostAsync(url, contenido);
            if (response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerializar<Tresp>(response);
                return new ChatMemberRespuesta<Tresp>(respuesta, false, response);
            }
            else
            {
                return new ChatMemberRespuesta<Tresp>(default, true, response);
            }
        }

        public async Task<ChatMemberRespuesta<object>> Delete(string url)
        {
            {
                var response = await http.DeleteAsync(url);


                return new ChatMemberRespuesta<object>(null,
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