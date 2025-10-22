using Microsoft.AspNetCore.SignalR;
using Proyecto2025.Shared.DTO;

namespace Proyecto2025.Server.Hubs
{
    public class ChatHub : Hub
    {
       
        // Enviar un mensaje a otro usuario o grupo
        public async Task EnviarMensaje(CrearMensajeDTO mensaje)
        {
        //    if (mensaje.EsGrupo)
        //    {
        //        // Enviar a todos los usuarios en el grupo
        //        await Clients.Group(mensaje.ReceptorId).SendAsync("RecibirMensaje", mensaje);
        //    }
        //    else
        //    {
        //        // Enviar directamente a un usuario (usamos su ConnectionId o mapeo de usuario)
        //        await Clients.User(mensaje.ReceptorId).SendAsync("RecibirMensaje", mensaje);
        //    }
        }

        // Enviar una notificación a un grupo o usuario específico
        public async Task EnviarNotificacion(NotificationDto notificacion)
        {
        //    if (!string.IsNullOrEmpty(notificacion.GrupoId))
        //    {
        //        await Clients.Group(notificacion.GrupoId).SendAsync("RecibirNotificacion", notificacion);
        //    }
        //    else if (!string.IsNullOrEmpty(notificacion.UsuarioId))
        //    {
        //        await Clients.User(notificacion.UsuarioId).SendAsync("RecibirNotificacion", notificacion);
        //    }
        //    else
        //    {
        //        // Notificación general (a todos)
        //        await Clients.All.SendAsync("RecibirNotificacion", notificacion);
        //    }
        }

        // Asignar al usuario al grupo de SignalR (al entrar a un grupo de chat)
        public async Task UnirseAGrupo(string grupoId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, grupoId);
        }

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

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Cleanup
            await base.OnDisconnectedAsync(exception);
        }
    }

}

