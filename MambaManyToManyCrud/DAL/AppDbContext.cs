
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
    }
}
