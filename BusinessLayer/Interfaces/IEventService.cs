using BusinessLayer.ModelDTOs;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IEventService
    {
        IEnumerable<EventDto> GetAll();
        EventDto GetById(Guid id);
        void Create(EventDto model);
        void Update(Guid id, EventDto model);
        void Delete(Guid id);
        IEnumerable<EventDto> GetTopRatedEvents(double minAverageRating = 4.0);
        IEnumerable<UserDto> GetParticipantsForEvent(Guid eventId);
        public IEnumerable<object> GetParticipantDetails(Guid eventId);
    }
}
