
using MambaManyToManyCrud.Configurations;
using MambaManyToManyCrud.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace MambaManyToManyCrud.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        public DbSet<Member>Members { get; set; }
        public DbSet<Profession> Professions { get; set;}
        public DbSet<MemberProfession> MembersProfessions { get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MemberConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            var datas=ChangeTracker.Entries<BaseEntity>();
            foreach (var item in datas)
            {
                var entity=item.Entity;
                switch (item.State)
                {
                    case EntityState.Modified:
                        entity.UpdatedDate = DateTime.UtcNow.AddHours(4);
                        break;
                    case EntityState.Added:
                        entity.UpdatedDate= DateTime.UtcNow.AddHours(4);
                        break;
                    default:
                        break;
                }
            }
            return base.SaveChanges();
        }
    }
}
