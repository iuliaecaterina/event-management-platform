using BusinessLayer.ModelDTOs;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface INotificationService
    {
        IEnumerable<NotificationDto> GetAll();
        NotificationDto GetById(Guid id);
        void Create(NotificationDto model);
        void Update(Guid id, NotificationDto model);
        void Delete(Guid id);
    }
}
