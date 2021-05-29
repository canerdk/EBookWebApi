using EBookWebApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBookWebApi.DAL.Context
{
    public class EBookDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .; Database = ArtizekaEBook; Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Branch)
                .WithMany(b => b.Grades);
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}
