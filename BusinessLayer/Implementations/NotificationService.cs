using BusinessLayer.Interfaces;
using BusinessLayer.ModelDTOs;
using DataAccess.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<Notification> _repository;

        public NotificationService(IRepository<Notification> repository)
        {
            _repository = repository;
        }

        public IEnumerable<NotificationDto> GetAll()
        {
            var notifications = _repository.GetAll();
            return notifications.Select(n => new NotificationDto
            {
                Id = n.Id,
                Message = n.Message,
                SentAt = n.SentAt,
                EventId = n.EventId,
                Recipient = n.Recipient
            });
        }

        public NotificationDto GetById(Guid id)
        {
            var notification = _repository.GetById(id);
            if (notification == null) return null;

            return new NotificationDto
            {
                Id = notification.Id,
                Message = notification.Message,
                SentAt = notification.SentAt,
                EventId = notification.EventId,
                Recipient = notification.Recipient
            };
        }

        public void Create(NotificationDto model)
        {
            var entity = new Notification
            {
                Message = model.Message,
                SentAt = model.SentAt,
                EventId = model.EventId,
                Recipient = model.Recipient
            };

            _repository.Add(entity);
            _repository.SaveChanges();
        }

        public void Update(Guid id, NotificationDto model)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new KeyNotFoundException("Entity not found");

            entity.Message = model.Message;
            entity.SentAt = model.SentAt;
            entity.EventId = model.EventId;
            entity.Recipient = model.Recipient;

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
