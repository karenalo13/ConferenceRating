using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateItem.Models;

namespace TemplateItem.Data.EntityTypeConfiguration
{
    class ConferenceConfiguration : IEntityTypeConfiguration<RatingModel>
    {
        public void Configure(EntityTypeBuilder<RatingModel> builder)
        {
            builder.ToTable("RatingIulianAndrei").HasKey(x => new { x.Id });
        }
    }
}
