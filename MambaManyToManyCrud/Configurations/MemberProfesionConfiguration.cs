using MambaManyToManyCrud.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MambaManyToManyCrud.Configurations
{
    public class MemberProfesionConfiguration : IEntityTypeConfiguration<MemberProfession>
    {
        public void Configure(EntityTypeBuilder<MemberProfession> builder)
        {
            builder.HasOne(x => x.Member).WithMany(x => x.MemberProfessions);
            builder.HasOne(x => x.Profession).WithMany(x => x.MemberProfessions);
        }
    }
}