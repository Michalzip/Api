using System;
using Api.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.EF
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Tags> Tags { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }
    }
}
