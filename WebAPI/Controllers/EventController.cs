using BusinessLayer.Interfaces;
using BusinessLayer.ModelDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IUserService _userService; // pentru a obține detalii despre utilizator

        public EventController(IEventService eventService, IUserService userService)
        {
            _eventService = eventService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EventDto>> GetAll()
        {
            return Ok(_eventService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<EventDto> GetById(Guid id)
        {
            var ev = _eventService.GetById(id);
            if (ev == null) return NotFound();
            return Ok(ev);
        }

        [HttpPost]
        public IActionResult Create([FromBody] EventDto dto, [FromQuery] Guid userId)
        {
            var user = _userService.GetById(userId);
            if (user == null || user.Role != "Organizer")
                return Forbid("Only organizers can create events.");

            dto.OrganizerId = user.Id;
            dto.OrganizerName = user.Name;

            _eventService.Create(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] EventDto dto, [FromQuery] Guid userId)
        {
            var user = _userService.GetById(userId);
            if (user == null || user.Role != "Organizer")
                return Forbid("Only organizers can update events.");

            _eventService.Update(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id, [FromQuery] Guid userId)
        {
            var user = _userService.GetById(userId);
            if (user == null || user.Role != "Organizer")
                return Forbid("Only organizers can delete events.");

            _eventService.Delete(id);
            return NoContent();
        }

        [HttpGet("top-rated")]
        public ActionResult<IEnumerable<EventDto>> GetTopRated([FromQuery] double minRating = 4.0)
        {
            return Ok(_eventService.GetTopRatedEvents(minRating));
        }

        [HttpGet("{eventId}/participants")]
        public ActionResult<IEnumerable<UserDto>> GetParticipants(Guid eventId)
        {
            var participants = _eventService.GetParticipantsForEvent(eventId);
            return Ok(participants);
        }

        [HttpGet("{eventId}/participant-details")]
        public IActionResult GetParticipantDetails(Guid eventId)
        {
            var result = _eventService.GetParticipantDetails(eventId);
            return Ok(result);
        }
    }
}
