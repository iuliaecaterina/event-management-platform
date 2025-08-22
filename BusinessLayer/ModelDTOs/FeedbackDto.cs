using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ModelDTOs
{
    public class FeedbackDto
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public string ParticipantName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
