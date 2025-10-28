using Microsoft.AspNetCore.SignalR;
using Proyecto2025.Shared.DTO;
using System.Threading.Tasks;

namespace Proyecto2025.Server.Hubs
{
    public class ChatHub : Hub
    {
       
        // Enviar un mensaje a otro usuario o grupo
        public async Task EnviarMensaje(CrearMensajeDTO mensaje, ChatDTO chat)
        {

            if (chat.IsGroup== true)
            {
                // Enviar a todos los usuarios en el grupo
                //        await Clients.Group(mensaje.ReceptorId).SendAsync("RecibirMensaje", mensaje);
                await Clients.Group(chat.Id.ToString()).SendAsync("RecibirMensaje", mensaje);
                Console.WriteLine($"Mensaje enviado al grupo {chat.Id}");
            }
            else
            {
                // Enviar directamente a un usuario (usamos su ConnectionId o mapeo de usuario)
                //        await Clients.User(mensaje.ReceptorId).SendAsync("RecibirMensaje", mensaje);
                await Clients.Group(chat.Id.ToString()).SendAsync("RecibirMensaje", mensaje);
                Console.WriteLine($"Mensaje enviado al chat individual {chat.Id}");
            }
        }

        // Enviar una notificación a un grupo o usuario específico
        public async Task EnviarNotificacion(NotificationDto notificacion, ChatDTO chat)
        {
            if (chat.IsGroup == true)
            {
              // Enviar notificación solo al usuario destino (usando su grupo personal)
                await Clients.Group(chat.Id.ToString())
                                        .SendAsync("RecibirNotificacion", notificacion);

                Console.WriteLine($"Notificación enviada a grupo {chat.Id}: {notificacion.Message}");
            } else {
                await Clients.Group(chat.Id.ToString())
                                        .SendAsync("RecibirNotificacion", notificacion);

                Console.WriteLine($"Notificación enviada a grupo {chat.Id}: {notificacion.Message}");
            }
        }


        // Llamado cuando un usuario se conecta, para que se una a sus grupos/chats
        public async Task UnirseAlChat(ChatDTO chat)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chat.Id.ToString());
            Console.WriteLine($"Usuario conectado al chat {chat.Id}");
        }

        // Cada usuario también puede tener un grupo propio para notificaciones personales
        //public async Task UnirseANotificaciones(string userId)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        //    Console.WriteLine($"Usuario {userId} se unió a su grupo de notificaciones");
        //}

        // Quitar al usuario del grupo (al salir del grupo de chat)
        public async Task SalirDeGrupo(string grupoId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, grupoId);
        }

        // Opcional: mapear usuario a conexión, en caso de que el usuario se conecte desde multiples dispositivos
        public override async Task OnConnectedAsync()
        {
            // Puedes mapear aquí el usuario al Context.UserIdentifier si está autenticado
            await base.OnConnectedAsync();
        }

        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    // Cleanup
        //    await base.OnDisconnectedAsync(exception);
        //}
    }

}

