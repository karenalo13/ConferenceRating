using Microsoft.EntityFrameworkCore;
using TemplateItem.Models;

#nullable disable

namespace TemplateItem.Data
{
    public partial class PaymentGatewayContext : DbContext
    {
        public DbSet<RatingModel> RatingModels { get; set; }


        public PaymentGatewayContext(DbContextOptions<PaymentGatewayContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new ConferenceConfiguration());
        }
    }
}
