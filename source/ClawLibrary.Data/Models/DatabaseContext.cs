using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace ClawLibrary.Data.Models
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplate { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasIndex(e => new { e.FirstName, e.LastName })
                    .HasName("IX_Author_FirstName_LastName")
                    .IsUnique();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CreatedDate);
              
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ModifiedBy).HasMaxLength(256);

                entity.Property(e => e.ModifiedDate);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.ImageFile)
                    .WithMany(p => p.Author)
                    .HasForeignKey(d => d.ImageFileId)
                    .HasConstraintName("FK_Author_File");
            });

            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.Property(e => e.Content).HasMaxLength(5000);

                entity.Property(e => e.Content).HasMaxLength(255);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.Isbn)
                    .HasName("IX_Book_ISBN")
                    .IsUnique();

                entity.HasIndex(e => e.Title)
                    .HasName("IX_Book_Title")
                    .IsUnique();
                
                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CreatedDate);

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("ISBN")
                    .HasMaxLength(50);

                entity.Property(e => e.Language).HasMaxLength(2);

                entity.Property(e => e.ModifiedBy).HasMaxLength(256);

                entity.Property(e => e.ModifiedDate);

                entity.Property(e => e.PublishDate);

                entity.Property(e => e.Publisher).HasMaxLength(256);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Book_Author");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Book_Category");

                entity.HasOne(d => d.ImageFile)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.ImageFileId)
                    .HasConstraintName("FK_Book_File");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX_Book_Title")
                    .IsUnique();
                
                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CreatedDate);

                entity.Property(e => e.ModifiedBy).HasMaxLength(256);

                entity.Property(e => e.ModifiedDate);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasIndex(e => e.FileName)
                    .HasName("IX_File_Name")
                    .IsUnique();
                
                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CreatedDate);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.ModifiedBy).HasMaxLength(256);

                entity.Property(e => e.ModifiedDate);

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

                entity.Property(e => e.CreatedDate);

                entity.Property(e => e.ModifiedBy).HasMaxLength(256);

                entity.Property(e => e.ModifiedDate);

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

                entity.Property(e => e.Language).HasMaxLength(2);
                
                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CreatedDate);

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

                entity.Property(e => e.ModifiedDate);

                entity.Property(e => e.PasswordResetKey).HasMaxLength(72);

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

                entity.Property(e => e.CreatedDate);

                entity.Property(e => e.ModifiedBy).HasMaxLength(256);

                entity.Property(e => e.ModifiedDate);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FKUserRole_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FKUserRole_UserId");
            });
        }
    }
}