using TemplateItem.Data;

namespace RatingSystem.Application.Services
{
    public class NewIban
    {
        private readonly PaymentGatewayContext _dbContext;

        public NewIban(PaymentGatewayContext dbContext)
        {
            _dbContext = dbContext;
        }

        
    }
}
