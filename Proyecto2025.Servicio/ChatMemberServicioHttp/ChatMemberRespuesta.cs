using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025.Servicio.ChatMemberServicioHttp
{
    public class ChatMemberRespuesta<T>
    {
        public T? Respuesta { get; }
        public bool Error { get; }
        public HttpResponseMessage HttpResponseMessage { get; set; }
        public ChatMemberRespuesta(T? respuesta,
                                      bool error, HttpResponseMessage httpResponseMessage)
        {
            Respuesta = respuesta;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }

        public string OptenerError()
        {
            if (!Error)
            {
                return string.Empty;
            }
            else
            {
                var statuscode = HttpResponseMessage.StatusCode;
                switch (statuscode)
                {
                    case HttpStatusCode.NotFound:
                        return "Solicitud incorrecta. Verifique los datos enviados.";
                    case HttpStatusCode.Unauthorized:
                        return "No autorizado. Verifique sus credenciales.";
                    case HttpStatusCode.Forbidden:
                        return "Prohibido. No tiene permiso para acceder a este recurso.";
                    case HttpStatusCode.BadRequest:
                        return "Recurso no encontrado. Verifique la URL.";
                    default:
                        return $"Error desconocido: {statuscode}";
                }
            }
        }
    }
}
