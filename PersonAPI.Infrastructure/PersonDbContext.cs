using Microsoft.EntityFrameworkCore;
using PersonAPI.Core.Entity;
using System;

namespace PersonAPI.Infrastructure
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
        {
        }

        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<PhoneNumberEntity> PhoneNumbers { get; set; }
        public DbSet<RelatedPersonEntity> RelatedPersons { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<LogEntity> Logs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RelatedPersonEntity>()
                .HasOne(rp => rp.Person)
                .WithMany(p => p.RelatedPersons)
                .HasForeignKey(rp => rp.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RelatedPersonEntity>()
                .HasOne(rp => rp.RelatedPerson)
                .WithMany()
                .HasForeignKey(rp => rp.RelatedPersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CityEntity>().HasData(
                new CityEntity { Id = 1, NameKa = "თბილისი", NameEn = "Tbilisi" },
                new CityEntity { Id = 2, NameKa = "ქუთაისი", NameEn = "Kutaisi" },
                new CityEntity { Id = 3, NameKa = "ბათუმი", NameEn = "Batumi" },
                new CityEntity { Id = 4, NameKa = "რუსთავი", NameEn = "Rustavi" }
            );
        }
    }
}
