using BusinessLayer.ModelDTOs;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IRegistrationService
    {
        IEnumerable<RegistrationDto> GetAll();
        RegistrationDto GetById(Guid id);
        IEnumerable<UserDto> GetUsersByEventId(Guid eventId);
        bool IsUserRegistered(Guid eventId, Guid userId);
        void Create(RegistrationDto model);
        void Update(Guid id, RegistrationDto model);
        void Delete(Guid id);
    }
}
