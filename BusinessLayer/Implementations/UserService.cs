using BusinessLayer.Interfaces;
using BusinessLayer.ModelDTOs;
using DataAccess.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = _repository.GetAll();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            });
        }

        public UserDto GetById(Guid id)
        {
            var user = _repository.GetById(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

        public void Create(UserDto model)
        {
            var entity = new User
            {
                Name = model.Name,
                Email = model.Email,
                Role = model.Role
            };

            _repository.Add(entity);
            _repository.SaveChanges();
        }

        public void Update(Guid id, UserDto model)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new KeyNotFoundException("Entity not found");

            entity.Name = model.Name;
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
    }
}
