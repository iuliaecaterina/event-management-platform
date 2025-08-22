using BusinessLayer.ModelDTOs;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IFeedbackService
    {
        IEnumerable<FeedbackDto> GetAll();
        FeedbackDto GetById(Guid id);
        void Create(FeedbackDto model);
        void Update(Guid id, FeedbackDto model);
        void Delete(Guid id);
    }
}
