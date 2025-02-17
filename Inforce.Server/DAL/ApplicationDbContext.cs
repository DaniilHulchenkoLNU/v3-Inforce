namespace Inforce.Server.DAL;

using Inforce.Server.Domain;
using Microsoft.EntityFrameworkCore;
using System;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<ShortenedUrl> Urls { get; set; }
    public DbSet<About> About { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<About>().HasData(
           new About
           {
               Id = 1,
               Description = "The URL shortening algorithm works by generating a unique short code for each original URL. " +
                             "When a user submits a URL to be shortened, the system checks if the URL already exists in the database. " +
                             "If it does, the existing short code is returned. If not, a new short code is generated using a combination " +
                             "of alphanumeric characters. This short code is then stored in the database along with the original URL. " +
                             "When a user accesses the short URL, the system retrieves the original URL from the database and redirects " +
                             "the user to it."
           }
       );


        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();


        modelBuilder.Entity<ShortenedUrl>()
            .HasIndex(u => u.ShortCode)
            .IsUnique();


        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "admin", PasswordHash = "$2a$11$X9OtbQvxRZCzqM3JmXZ.VuI5chhmf1IuVVklEoXx0xdFk7QheODW2", Role = "Admin" },
            new User { Id = 2, Username = "testuser", PasswordHash = "$2a$11$0iI2gMjN9SOE3KNZ9H25yep5xRhn3LU.y2KP6eIt7n09YOL6MC1Ty", Role = "User" }
        );


        modelBuilder.Entity<ShortenedUrl>().HasData(
            new ShortenedUrl { Id = 1, OriginalUrl = "https://www.google.com", ShortCode = "ggl123", CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0), CreatedBy = "admin" },
            new ShortenedUrl { Id = 2, OriginalUrl = "https://www.github.com", ShortCode = "ghb456", CreatedAt = new DateTime(2024, 1, 2, 15, 30, 0), CreatedBy = "testuser" },
            new ShortenedUrl { Id = 3, OriginalUrl = "https://www.microsoft.com", ShortCode = "ms789", CreatedAt = new DateTime(2024, 1, 3, 18, 45, 0), CreatedBy = "admin" }
        );
    }
}
