using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using prueba.entities;

namespace prueba
{
    public class AplicationDBContex : DbContext
    {
        public AplicationDBContex(DbContextOptions<AplicationDBContex> options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<book> Books { get; set; }

    }
}