using BusinessLayer.Interfaces;
using BusinessLayer.ModelDTOs;
using DataAccess.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IRepository<Feedback> _repository;


        public FeedbackService(IRepository<Feedback> repository)
        {
            _repository = repository;
        }

        public IEnumerable<FeedbackDto> GetAll()
        {
            var feedbacks = _repository.GetAll();
            return feedbacks.Select(f => new FeedbackDto
            {
                Id = f.Id,
                Rating = f.Rating,
                Comment = f.Comment,
                EventId = f.EventId,
                ParticipantName = f.ParticipantName
            });
        }

        public FeedbackDto GetById(Guid id)
        {
            var feedback = _repository.GetById(id);
            if (feedback == null) return null;

            return new FeedbackDto
            {
                Id = feedback.Id,
                Rating = feedback.Rating,
                Comment = feedback.Comment,
                EventId = feedback.EventId,
                ParticipantName = feedback.ParticipantName
            };
        }

        public void Create(FeedbackDto model)
        {
            if (model.Rating < 1 || model.Rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5.");

            if (string.IsNullOrWhiteSpace(model.Comment))
                throw new ArgumentException("Comment must not be empty.");

            var entity = new Feedback
            {
                Rating = model.Rating,
                Comment = model.Comment,
                EventId = model.EventId,
                ParticipantName = model.ParticipantName
            };

            _repository.Add(entity);
            _repository.SaveChanges();
        }

        public void Update(Guid id, FeedbackDto model)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new KeyNotFoundException("Entity not found");

            entity.Rating = model.Rating;
            entity.Comment = model.Comment;
            entity.EventId = model.EventId;
            entity.ParticipantName = model.ParticipantName;

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
