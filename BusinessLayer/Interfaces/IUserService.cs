using BusinessLayer.ModelDTOs;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAll();
        UserDto GetById(Guid id);
        void Create(UserDto model);
        void Update(Guid id, UserDto model);
        void Delete(Guid id);
    }
}
