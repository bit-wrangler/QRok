using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRok.Models
{
    public class QRokContext : DbContext
    {
        public DbSet<Survey> Surveys { get; set; }

        public QRokContext(DbContextOptions<QRokContext> dbContextOptions)
            :base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Survey>()
                .Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Survey>()
                .Property(s => s.Guid)
                .IsRequired();
        }
    }
}
