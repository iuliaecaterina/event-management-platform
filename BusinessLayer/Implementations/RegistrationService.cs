using BusinessLayer.Interfaces;
using BusinessLayer.ModelDTOs;
using DataAccess;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Implementations
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRepository<Registration> _repository;
        private readonly MyDbContext _context;

        public RegistrationService(IRepository<Registration> repository, MyDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public IEnumerable<RegistrationDto> GetAll()
        {
            var registrations = _repository.GetAll();
            return registrations.Select(r => new RegistrationDto
            {
                Id = r.Id,
                EventId = r.EventId,
                UserId = r.Participant?.Id ?? Guid.Empty,
                ParticipantName = r.ParticipantName,
                Email = r.Email,
                Role = r.Role
            });
        }

        public RegistrationDto GetById(Guid id)
        {
            var registration = _repository.GetById(id);
            if (registration == null) return null;

            return new RegistrationDto
            {
                Id = registration.Id,
                EventId = registration.EventId,
                UserId = registration.Participant?.Id ?? Guid.Empty,
                ParticipantName = registration.ParticipantName,
                Email = registration.Email,
                Role = registration.Role
            };
        }

        public void Create(RegistrationDto model)
        {
            var entity = new Registration
            {
                EventId = model.EventId,
                UserId = model.UserId,
                ParticipantName = model.ParticipantName,
                Email = model.Email,
                Role = model.Role
            };

            _repository.Add(entity);
            _repository.SaveChanges();
        }

        public void Update(Guid id, RegistrationDto model)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new KeyNotFoundException("Entity not found");

            entity.EventId = model.EventId;
            entity.UserId = model.UserId;
            entity.ParticipantName = model.ParticipantName;
            entity.Email = model.Email;
            entity.Role = model.Role;

            _repository.Update(entity);
            _repository.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new KeyNotFoundException("Entity not found");

            _repository.Remove(entity);
            _repository.SaveChanges();
        }
        public IEnumerable<UserDto> GetUsersByEventId(Guid eventId)
        {
            var registrations = _context.Registrations
                .Include(r => r.Participant)
                .Where(r => r.EventId == eventId && r.Participant != null)
                .Select(r => new UserDto
                {
                    Id = r.Participant.Id,
                    Name = r.Participant.Name,
                    Email = r.Participant.Email,
                    Role = r.Participant.Role
                })
                .ToList();

            return registrations;
        }
        public bool IsUserRegistered(Guid eventId, Guid userId)
        {
            return _context.Registrations.Any(r => r.EventId == eventId && r.UserId == userId);
        }

    }
}
