using MediatR;

namespace RatingSystem.PublishedLanguage.Events
{
    public class RatingMade: INotification
    {
        public int rating { get; set; }
    }
}