using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime SentAt { get; set; }

        public Guid EventId { get; set; }

        public string Recipient { get; set; }

        public Event Event { get; set; }
    }
}
