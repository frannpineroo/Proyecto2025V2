﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.BD.Datos.Entity
{
    public class User : EntityBase
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public required string Password { get; set; }

        public bool IsOnline { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public long? OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<ChatMember> ChatMemberships { get; set; } = new List<ChatMember>();
        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<ExternalIntegration> ExternalIntegrations { get; set; } = new List<ExternalIntegration>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
