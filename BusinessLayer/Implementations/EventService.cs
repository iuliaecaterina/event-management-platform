using BusinessLayer.Interfaces;
using BusinessLayer.ModelDTOs;
using DataAccess;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Implementations
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> _repository;
        private readonly MyDbContext _context;
        private readonly IMemoryCache _cache;
        public EventService(IRepository<Event> repository, MyDbContext context, IMemoryCache cache)
        {
            _repository = repository;
            _context = context;
            _cache = cache;
        }

        public IEnumerable<EventDto> GetAll()
        {
            var events = _repository.GetAll();
            return events.Select(e => new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Date = e.Date,
                Location = e.Location,
                OrganizerName = e.OrganizerName,
                OrganizerId = e.OrganizerId
            });
        }

        public EventDto GetById(Guid id)
        {
            var ev = _repository.GetById(id);
            if (ev == null) return null;

            return new EventDto
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                Date = ev.Date,
                Location = ev.Location,
                OrganizerName = ev.OrganizerName,
                OrganizerId = ev.OrganizerId
            };
        }

        public void Create(EventDto model)
        {
            var entity = new Event
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Description = model.Description,
                Date = model.Date,
                Location = model.Location,
                OrganizerId = model.OrganizerId ?? Guid.Empty,
                OrganizerName = model.OrganizerName
            };
            _repository.Add(entity);
            _repository.SaveChanges();
        }

        public void Update(Guid id, EventDto model)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new KeyNotFoundException("Entity not found");

            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.Date = model.Date;
            entity.Location = model.Location;
            entity.OrganizerName = model.OrganizerName;
            entity.OrganizerId = model.OrganizerId ?? Guid.Empty;

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

        public IEnumerable<EventDto> GetTopRatedEvents(double minAverageRating = 4.0)
        {
            string cacheKey = $"topRatedEvents_{minAverageRating}";

            if (!_cache.TryGetValue(cacheKey, out List<EventDto> cachedEvents))
            {
                var events = _context.Events
                    .Include(e => e.Feedbacks)
                    .Where(e => e.Feedbacks.Any())
                    .Where(e => e.Feedbacks.Average(f => f.Rating) >= minAverageRating)
                    .OrderByDescending(e => e.Feedbacks.Average(f => f.Rating))
                    .ToList();

                cachedEvents = events.Select(e => new EventDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Date = e.Date,
                    Location = e.Location,
                    OrganizerName = e.OrganizerName,
                    OrganizerId = e.OrganizerId
                }).ToList();

                // set expiration
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(cacheKey, cachedEvents, cacheEntryOptions);
            }

            return cachedEvents;
        }
        public IEnumerable<UserDto> GetParticipantsForEvent(Guid eventId)
        {
            var users = _context.EventUsers
                .Where(eu => eu.EventId == eventId)
                .Select(eu => eu.User)
                .ToList();

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            });
        }
        public IEnumerable<object> GetParticipantDetails(Guid eventId)
        {
            var result = from eu in _context.EventUsers
                         join u in _context.Users on eu.UserId equals u.Id
                         where eu.EventId == eventId
                         select new
                         {
                             u.Name,
                             u.Email,
                             u.Role
                         };

            return result.ToList();
        }


    }
}
