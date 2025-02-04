using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryDemo.Domain
{
    public class AppDbContext : DbContext
    {

        public DbSet<Student> Students { get; set; }

        public DbSet<Library> Libraries { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
          .HasOne(b => b.Library)
          .WithMany(l => l.Books)
          .HasForeignKey(b => b.LibraryId);

            modelBuilder.Entity<Book>()
           .HasMany(b => b.Authors)
           .WithMany(a => a.Books);


        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {

                options.Equals("Data Source=c:/sqlite/students-books2025-02-04.db");

            }

        }

    }
}
