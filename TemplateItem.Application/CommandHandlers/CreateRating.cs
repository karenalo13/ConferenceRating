using MediatR;
using RatingSystem.Application.Services;
using RatingSystem.PublishedLanguage.Commands;
using RatingSystem.PublishedLanguage.Events;
using System;
using System.Threading;
using System.Threading.Tasks;
using TemplateItem.Data;
using TemplateItem.Models;
using System.Linq;

namespace RatingSystem.Application.WriteOperations
{
    public class CreateRating : IRequestHandler<MakeNewRating>
    {
        private readonly IMediator _mediator;
        private readonly ConferenceOptions _conferenceOptions;
        private readonly PaymentGatewayContext _dbContext;

        public CreateRating(IMediator mediator, ConferenceOptions conferenceOptions, PaymentGatewayContext dbContext)
        {
            _mediator = mediator;
            _conferenceOptions = conferenceOptions;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(MakeNewRating request, CancellationToken cancellationToken)
        {
            var raw = _dbContext.RatingModels.FirstOrDefault(e=> e.ConferenceId==request.ConferenceId && e.AttendeeEmail==request.AttendeeEmail);
            
            if (raw == null)
            {
                var account = new RatingModel
                {
                    AttendeeEmail = request.AttendeeEmail,
                    ConferenceId = request.ConferenceId,
                    Rating = request.Rating
                };
                _dbContext.RatingModels.Add(account);
                _dbContext.SaveChanges();

                RatingMade ec = new RatingMade
                {
                    rating = request.Rating
                };
                await _mediator.Publish(ec, cancellationToken);
                return Unit.Value;
            }

            else
            {

                raw.Rating = request.Rating;
                //var account = new RatingModel
                //{
                //    AttendeeEmail = request.AttendeeEmail,
                //    ConferenceId = request.ConferenceId,
                //    Rating = request.Rating
                //};
                //_dbContext.RatingModels.Update(account);
                _dbContext.SaveChanges();

                RatingMade ec = new RatingMade
                {
                    rating = raw.Rating
                };

                await _mediator.Publish(ec, cancellationToken);
                return Unit.Value;
            }

           

            
        }        
    }
}