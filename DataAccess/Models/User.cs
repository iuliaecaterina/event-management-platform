using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{

    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public ICollection<Event> CreatedEvents { get; set; } = new List<Event>();

        // Participant: poate participa la mai multe evenimente
        public ICollection<Event> AttendedEvents { get; set; } = new List<Event>();

        public ICollection<EventUser> EventUsers { get; set; } = new List<EventUser>();
    }
}
