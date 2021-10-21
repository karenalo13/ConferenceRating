using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TemplateItem.Data;

using System.Linq;




namespace RatingSystem.Application.Queries
{
    public class ListOfRatings
    {
        public class Validator : AbstractValidator<Query>
        {
            public Validator(PaymentGatewayContext _dbContext)
            {
                //RuleFor(q => q).Must(query =>
                //{
                //    var person = query.PersonId.HasValue ?
                //    _dbContext.Persons.FirstOrDefault(x => x.Id == query.PersonId) :
                //    _dbContext.Persons.FirstOrDefault(x => x.Cnp == query.Cnp);

                //    return person != null;
                //}).WithMessage("Customer not found");
                

                
            }
        }
       

        public class Query : IRequest<List<Model>>
        {
           public int ConferenceId { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, List<Model>>
        {
            private readonly PaymentGatewayContext _dbContext;
            private readonly ConferenceOptions _conferenceOptions;
            public QueryHandler(PaymentGatewayContext dbContext, ConferenceOptions conferenceOptions)
            {
                _dbContext = dbContext;
                _conferenceOptions = conferenceOptions;
            }

            public Task<List<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                var all = _dbContext.RatingModels.Where(e => e.ConferenceId == request.ConferenceId);
                var value = all.Sum(e => e.Rating);
                var count = all.Count();


                //var datas=all.ToList();
                //var result = datas.Select(x => new Model
                //{
                //    Id = x.Id,
                //    Rating=x.Rating,
                //    ConferenceId=x.ConferenceId,
                //    AttendeeEmail=x.AttendeeEmail
                //}).ToList();

                var result = new List<Model>();
                var model = new Model();
                model.Value = (double) ((double)value / (double)count);
                result.Add(model);


                return Task.FromResult(result);
            }
        }

        public class Model
        {
            //public int Id { get; set; }
            //public int Rating { get; set; }
            //public int ConferenceId { get; set; }
            //public string AttendeeEmail { get; set; }

            public double Value { get; set; }
            

        }
    }
}
