using Microsoft.EntityFrameworkCore;

namespace Library_Api.Entity
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Tag> Tags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<User>()
                 .Property(u => u.Email)
                 .IsRequired()
                 .HasMaxLength(255);
            modelBuilder.Entity<User>()
                 .Property(u => u.FirstName)
                 .IsRequired()
                 .HasMaxLength(25);
            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired();
            //Book
            modelBuilder.Entity<Book>()
                .Property(b => b.Tittle)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Book>()
                .Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Tags)
                .WithMany(t => t.Books);
        }
    }
}