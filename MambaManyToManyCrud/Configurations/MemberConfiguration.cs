
using MambaManyToManyCrud.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MambaManyToManyCrud.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(x=>x.FullName).IsRequired().HasMaxLength(50);
            builder.Property(x=>x.ImageUrl).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.LinkUrl).IsRequired().HasMaxLength(100);

        }
    }
}
