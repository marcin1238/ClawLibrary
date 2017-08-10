using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClawLibrary.Data.Models
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>(entity =>
            {
                entity.HasIndex(e => e.FileName)
                    .HasName("IX_File_Name")
                    .IsUnique();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("sysdatetimeoffset()");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.ModifiedBy).HasMaxLength(256);

                entity.Property(e => e.ModifiedDate).HasDefaultValueSql("sysdatetimeoffset()");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX_Role_Name")
                    .IsUnique();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("sysdatetimeoffset()");

                entity.Property(e => e.ModifiedBy).HasMaxLength(256);

                entity.Property(e => e.ModifiedDate).HasDefaultValueSql("sysdatetimeoffset()");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("IX_User_Email")
                    .IsUnique();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("sysdatetimeoffset()");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(256);

                entity.Property(e => e.ModifiedDate).HasDefaultValueSql("sysdatetimeoffset()");

                entity.Property(e => e.PasswordResetKey).HasMaxLength(72);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.ImageFile)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.ImageFileId)
                    .HasConstraintName("FK_User_File");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.RoleId })
                    .HasName("IXUserRole_Name")
                    .IsUnique();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("sysdatetimeoffset()");

                entity.Property(e => e.ModifiedBy).HasMaxLength(256);

                entity.Property(e => e.ModifiedDate).HasDefaultValueSql("sysdatetimeoffset()");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKUserRole_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FKUserRole_UserId");
            });
        }
    }
}