using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ModelDTOs
{
    public class RegistrationDto
    {
        public Guid? Id { get; set; }
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }

        public string ParticipantName { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }
    }

}