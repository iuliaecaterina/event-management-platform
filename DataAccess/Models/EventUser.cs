using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class EventUser
    {
        public Guid EventId { get; set; }
        public virtual Event Event { get; set; }   

        public Guid UserId { get; set; }
        public virtual User User { get; set; }   
    }

}
