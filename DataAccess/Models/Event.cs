using DataAccess.Models;

public class Event : BaseEvent
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public string OrganizerName { get; set; }

    public Guid OrganizerId { get; set; }
    public virtual User Organizer { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    public virtual ICollection<EventUser> EventUsers { get; set; } = new List<EventUser>();

}
