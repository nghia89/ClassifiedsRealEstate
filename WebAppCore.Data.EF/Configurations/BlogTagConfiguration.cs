using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppCore.Data.EF.Extensions;
using WebAppCore.Data.Entities;

namespace WebAppCore.Data.EF.Configurations
{
    public class BlogTagConfiguration : DbEntityConfiguration<ClassifiedsTag>
    {
        public override void Configure(EntityTypeBuilder<ClassifiedsTag> entity)
        {
            entity.Property(c => c.TagId).HasMaxLength(50).IsRequired()
            .IsUnicode(false).HasMaxLength(50);
            // etc.
        }
    }
}