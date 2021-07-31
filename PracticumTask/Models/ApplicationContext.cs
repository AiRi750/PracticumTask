using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>()
                .HasIndex(b => b.Title)
                .IsUnique();

            builder.Entity<Genre>()
                .HasIndex(g => g.Name)
                .IsUnique();

            builder.Entity<Genre>()
                .HasData(new Genre() { Id = 1, Name = "Novel" });

            builder.Entity<Genre>()
                .HasData(new Genre() { Id = 2, Name = "Fiction" });

            builder.Entity<Genre>()
                .HasData(new Genre() { Id = 3, Name = "Adventure" });

            builder.Entity<Author>()
                .HasData(new Author()
                {
                    Id = 1,
                    FirstName = "Иван",
                    LastName = "Иванов",
                    Patronymic = "Иванович",
                    Birthdate = new DateTime(1954, 4, 24)
                });

            builder.Entity<Author>()
                .HasData(new Author()
                {
                    Id = 2,
                    FirstName = "Пётр",
                    LastName = "Петров",
                    Birthdate = new DateTime(1968, 7, 11)
                });

            builder.Entity<Author>()
                .HasData(new Author()
                {
                    Id = 3,
                    FirstName = "Тумба Юмба",
                    Birthdate = new DateTime(1943, 6, 2)
                });

            builder.Entity<Book>()
                .HasData(new Book() { Id = 1, Title = "На волнах галоперидола", AuthorId = 3, GenreId = 3 });

            builder.Entity<Book>()
                .HasData(new Book() { Id = 2, Title = "Плачут ли программисты", AuthorId = 2, GenreId = 1 });

            builder.Entity<Book>()
                .HasData(new Book() { Id = 3, Title = "Подебажим?", AuthorId = 1, GenreId = 3 });

            //todo - unique author name?
        }
    }
}
