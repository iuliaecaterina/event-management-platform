using System;

namespace BusinessLayer.ModelDTOs
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
        public Guid EventId { get; set; }
        public string Recipient { get; set; }
        public Guid UserId { get; set; }
    }
}
