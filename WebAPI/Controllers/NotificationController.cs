using BusinessLayer.Interfaces;
using BusinessLayer.ModelDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IRegistrationService _registrationService;
        private readonly IEventService _eventService;
        private readonly IUserService _userService;

        public NotificationController(
            INotificationService notificationService,
            IRegistrationService registrationService,
            IEventService eventService,
            IUserService userService)
        {
            _notificationService = notificationService;
            _registrationService = registrationService;
            _eventService = eventService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<NotificationDto>> GetAll()
        {
            return Ok(_notificationService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<NotificationDto> GetById(Guid id)
        {
            var notification = _notificationService.GetById(id);
            if (notification == null) return NotFound();
            return Ok(notification);
        }

        [HttpPost]
        public IActionResult Create([FromBody] NotificationDto dto, [FromQuery] Guid userId)
        {
            var user = _userService.GetById(userId);
            if (user == null || user.Role != "Organizer")
                return Forbid("Only organizers can send notifications.");

            var ev = _eventService.GetById(dto.EventId);
            if (ev == null || ev.OrganizerId != user.Id)
                return Forbid("You can only send notifications for your own events.");

            // Obține participanții la acel eveniment
            var registeredUsers = _registrationService.GetUsersByEventId(dto.EventId);
            foreach (var participant in registeredUsers)
            {
                var notification = new NotificationDto
                {
                    EventId = dto.EventId,
                    UserId = participant.Id,
                    Message = dto.Message,
                    SentAt = DateTime.Now
                };

                _notificationService.Create(notification);
            }

            return Ok("Notifications sent to all registered users.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, NotificationDto dto)
        {
            _notificationService.Update(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _notificationService.Delete(id);
            return NoContent();
        }
    }
}
