using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public string ParticipantName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}
