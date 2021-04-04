using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Core;


namespace DAL.Data
{
    public partial class RJGDbContext : DbContext
    {
        public RJGDbContext()
        {
        }

        public RJGDbContext(DbContextOptions<RJGDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleMenu> RoleMenu { get; set; }
        public virtual DbSet<RoleUser> RoleUser { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Action).IsFixedLength(true);

                entity.Property(e => e.Area).IsFixedLength(true);

                entity.Property(e => e.Controller).IsFixedLength(true);

                entity.Property(e => e.IsDisable).HasDefaultValueSql("((1))");

                entity.Property(e => e.ParentId).HasDefaultValueSql("((-1))");
            });

            modelBuilder.Entity<RoleMenu>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.MeunId });

                entity.HasOne(d => d.Meun)
                    .WithMany(p => p.RoleMenu)
                    .HasForeignKey(d => d.MeunId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleMenu_Menu");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleMenu)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleMenu_Role");
            });

            modelBuilder.Entity<RoleUser>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.UserId });

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleUser)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleUser_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RoleUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleUser_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Account).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}