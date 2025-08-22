using System;

namespace DataAccess.Models
{
    public class Registration
    {
        public Guid Id { get; set; }
        public string ParticipantName { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public Guid? UserId { get; set; } 
        public User Participant { get; set; }
    }
}
