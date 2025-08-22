using BusinessLayer.Interfaces;
using BusinessLayer.ModelDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IRegistrationService _registrationService;

        public FeedbackController(
            IFeedbackService feedbackService,
            IRegistrationService registrationService)
        {
            _feedbackService = feedbackService;
            _registrationService = registrationService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FeedbackDto>> GetAll()
        {
            return Ok(_feedbackService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<FeedbackDto> GetById(Guid id)
        {
            var feedback = _feedbackService.GetById(id);
            if (feedback == null) return NotFound();
            return Ok(feedback);
        }

        //Verifica dacă user-ul este inregistrat la eveniment
        [HttpPost]
        public IActionResult Create([FromBody] FeedbackDto dto, [FromQuery] Guid userId)
        {
            var isRegistered = _registrationService.IsUserRegistered(dto.EventId, userId);

            if (!isRegistered)
                return Forbid("Only registered participants can leave feedback.");

            _feedbackService.Create(dto);
            return Ok("Feedback submitted.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, FeedbackDto dto)
        {
            _feedbackService.Update(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _feedbackService.Delete(id);
            return NoContent();
        }
    }
}
