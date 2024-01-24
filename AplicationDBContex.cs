using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel;

using prueba.entities;

namespace prueba
{
    public class AplicationDBContex : IdentityDbContext
    {
        public AplicationDBContex(DbContextOptions<AplicationDBContex> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Author_Book>().HasKey(al => new { al.AuthorId, al.BookId });
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Comments> comments { get; set; }
        public DbSet<Author_Book> Author_Book { get; set; }

    }
}