using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data.Models.Base;
using Data.Models.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Region> Regions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<LastReadedFile> LastReadedFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=DESKTOP-5KJ75V9\SQLEXPRESS;Database=TotalErrorDb;Trusted_Connection=True");
        }

        public override int SaveChanges()
        {
            var entries = this.ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && x.State == EntityState.Added || x.State == EntityState.Deleted).ToList();

            foreach (EntityEntry entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedAt = DateTime.Now;
                        break;

                    case EntityState.Deleted:
                        entity.DeletedAt = DateTime.Now;
                        entry.State = EntityState.Modified;
                        break;
                }
            }
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException(e.Message);
            }
        }
    }
}
