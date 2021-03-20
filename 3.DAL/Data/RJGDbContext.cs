using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Core;

namespace DAL.Data
{
    public partial class RJGDbContext : DbContext
    {
        public RJGDbContext(DbContextOptions<RJGDbContext> options)
            : base(options)
        {
        }

        // public virtual DbSet<Efmigrationshistory> Efmigrationshistory { get; set; }
        // public virtual DbSet<Role> Role { get; set; }
        // public virtual DbSet<Roleuser> Roleuser { get; set; }
        // public virtual DbSet<Test> Test { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
