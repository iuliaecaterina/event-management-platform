using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess
{
    public class MyDbContext : DbContext
    {
        private readonly string _windowsConnectionString =
            @"Server=.\SQLExpress;Database=TAP_DB_Project;Trusted_Connection=True;TrustServerCertificate=true";

        public DbSet<TestModel> TestModels { get; set; }

        // Implement different roles such as event organizer and participant.
        public DbSet<User> Users { get; set; }

        // Create new events with details like title, description, date, and location.
        public DbSet<Event> Events { get; set; }

        // Allow users to leave feedback and ratings for events they attended.
        public DbSet<Feedback> Feedbacks { get; set; }

        // Enable event organizers to send notifications and updates to attendees.
        public DbSet<Notification> Notifications { get; set; }

        // Allow users to register for events and manage their registrations.
        public DbSet<Registration> Registrations { get; set; }

        public DbSet<EventUser> EventUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
             
                .UseSqlServer(_windowsConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        
            // User - Event (organizer)
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Organizer)
                .WithMany(u => u.CreatedEvents)
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Event - Feedback (one-to-many)
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Event)
                .WithMany(e => e.Feedbacks)
                .HasForeignKey(f => f.EventId);

            // Event - Notification (one-to-many)
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Event)
                .WithMany(e => e.Notifications)
                .HasForeignKey(n => n.EventId);

            // Event - Registration (one-to-many)
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(r => r.EventId);

            // Registration - Participant (User)
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Participant)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-Many: Event - User (EventUser)
            modelBuilder.Entity<EventUser>().HasKey(eu => new { eu.EventId, eu.UserId });

            modelBuilder.Entity<EventUser>()
                .HasOne(eu => eu.Event)
                .WithMany(e => e.EventUsers)
                .HasForeignKey(eu => eu.EventId);

            modelBuilder.Entity<EventUser>()
                .HasOne(eu => eu.User)
                .WithMany(u => u.EventUsers)
                .HasForeignKey(eu => eu.UserId);

           
            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            var organizerId = Guid.Parse("f1f1f1f1-f1f1-f1f1-f1f1-f1f1f1f1f1f1");
            var participantId = Guid.Parse("a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2");
            var eventId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            // Seed Users 
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = organizerId,
                    Name = "Organizer One",
                    Email = "org@example.com",
                    Role = "Organizer"
                },
                new User
                {
                    Id = participantId,
                    Name = "Participant One",
                    Email = "participant@example.com",
                    Role = "Participant"
                }
            );

            // Seed Event
            modelBuilder.Entity<Event>().HasData(new Event
            {
                Id = eventId,
                Title = "Seeded Event",
                Description = "Initial demo event for testing",
                Date = new DateTime(2025, 5, 22, 10, 0, 0), // Calendar view: evenimente viitoare
                Location = "Virtual",
                OrganizerId = organizerId,
                OrganizerName = "Organizer One"
            });

            // Seed Registration
            modelBuilder.Entity<Registration>().HasData(new Registration
            {
                Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                EventId = eventId,
                UserId = participantId,
                ParticipantName = "Participant One",
                Email = "participant@example.com",
                Role = "Attendee"
            });

            // Seed Feedback
            modelBuilder.Entity<Feedback>().HasData(new Feedback
            {
                Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                EventId = eventId,
                ParticipantName = "Participant One",
                Rating = 5,
                Comment = "Great event!"
            });

            // Seed Notification
            modelBuilder.Entity<Notification>().HasData(new Notification
            {
                Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                EventId = eventId,
                Message = "Welcome to the event!",
                SentAt = new DateTime(2025, 5, 22, 10, 0, 0),
                Recipient = "participant@example.com"
            });

            // Seed many-to-many link
            modelBuilder.Entity<EventUser>().HasData(new EventUser
            {
                EventId = eventId,
                UserId = participantId
            });
        }
    }
}
