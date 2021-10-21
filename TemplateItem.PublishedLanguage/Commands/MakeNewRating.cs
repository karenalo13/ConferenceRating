using MediatR;

namespace RatingSystem.PublishedLanguage.Commands
{
    public class MakeNewRating : IRequest
    {
        public int ConferenceId { get; set; }
        public int Rating { get; set; }
        public string AttendeeEmail { get; set; }
    }
}
