
using BusinessLayer.Interfaces;
using BusinessLayer.ModelDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RegistrationDto>> GetAll()
        {
            return Ok(_registrationService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<RegistrationDto> GetById(Guid id)
        {
            var registration = _registrationService.GetById(id);
            if (registration == null) return NotFound();
            return Ok(registration);
        }

        [HttpPost]
        public IActionResult Create([FromBody] RegistrationDto dto)
        {
            if (dto.Id == Guid.Empty)
                dto.Id = Guid.NewGuid();

            _registrationService.Create(dto);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, RegistrationDto dto)
        {
            _registrationService.Update(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _registrationService.Delete(id);
            return NoContent();
        }
    }
}