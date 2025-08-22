
Event Management Platform

This application is a platform for managing events, allowing organizers and participants to interact through events, 
notifications, registrations, and feedback.

Implemented Features

-> Event creation with title, description, date, and location
-> Distinct roles: Organizer & Participant
-> Automatic notifications: organizers can send messages to participants
-> Feedback & ratings: only participants can submit feedback
-> Visual calendar (Blazor): upcoming events displayed in a calendar view
-> User registration for events and participant listing

Project Architecture

The solution is divided into two main components:
-> WebAPI (ASP.NET Core) – manages events, feedback, users, and notifications
-> Blazor WebAssembly – web interface for event management

Core Functionalities

->Full CRUD operations for:
	-> Events
	-> Feedback
	-> Users
	-> Registrations
-> Top events with rating > 4 (with caching)
-> List participants for each event
-> Blazor UI for creating, updating, and deleting events
-> Entity Framework Core with automatic migrations and seed data
-> Strategy Pattern for feedback validation

API Documentation (Swagger Enabled)

Event Endpoints
	GET    /api/event
	GET    /api/event/{id}
	POST   /api/event?userId={organizerId}
	PUT    /api/event/{id}?userId={organizerId}
	DELETE /api/event/{id}?userId={organizerId}
	GET    /api/event/top-rated
	GET    /api/event/{eventId}/participants

Feedback Endpoints
	GET    /api/feedback
	POST   /api/feedback?userId={participantId}

User Endpoints
	GET    /api/user
	POST   /api/user

Registration Endpoints
	POST   /api/registration
	GET    /api/registration

Notification Endpoint
	POST   /api/notification?userId={organizerId}

Example Data

	Example Event (POST /api/event)
{
  "id": "00000000-0000-0000-0000-000000000010",
  "title": "AI Intensive Workshop",
  "description": "Hands-on AI event",
  "date": "2025-06-01T10:00:00",
  "location": "Online",
  "organizerName": "Mihai",
  "organizerId": "f1f1f1f1-f1f1-f1f1-f1f1-f1f1f1f1f1f1"
}

	Example Feedback (POST /api/feedback)
{
  "id": "00000000-0000-0000-0000-000000000099",
  "eventId": "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
  "rating": 5,
  "comment": "Very interesting!",
  "participantName": "Participant One"
}
	Example User (POST /api/user)
{
  "id": "12345678-1111-1111-1111-123456789abc",
  "name": "Participant Two",
  "email": "newuser@example.com",
  "role": "Participant"
}

	Example Registration (POST /api/registration)
{
  "eventId": "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
  "userId": "a0003fc8-fb5e-4adc-abc8-08dd8b165fe8",
  "participantName": "New User",
  "email": "new@example.com",
  "role": "Attendee"
}

	Example Notification (POST /api/notification)
{
  "id": "eeeeeeee-eeee-eeee-eeee-eeeeeeeeee99",
  "eventId": "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
  "message": "Don't forget to attend!",
  "sentAt": "2025-05-22T10:00:00",
  "recipient": "participant@example.com"
}

Autor
- Nume: Dracea Iulia Ecaterina
