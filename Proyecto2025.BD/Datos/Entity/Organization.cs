using System;

namespace Proyecto2025.BD.Datos.Entity
{
    public class Organization
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public long? AdminId { get; set; }
        public User? Admin { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Chat> Chats { get; set; } = new List<Chat>();
        public ICollection<ChatStatistic> ChatStatistics { get; set; } = new List<ChatStatistic>();
    }
}
