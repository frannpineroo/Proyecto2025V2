namespace Proyecto2025.Servicio.ChatMemberServicioHttp
{
    public interface IChatMemberServicio
    {
        //falta implementar el put
        Task<ChatMemberRespuesta<T>> Get<T>(string url);
        Task<ChatMemberRespuesta<Tresp>> Post<T, Tresp>(string url, T enviar);
        Task<ChatMemberRespuesta<object>> Delete(string url);
    }
}