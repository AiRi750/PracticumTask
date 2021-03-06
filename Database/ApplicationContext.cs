using Microsoft.EntityFrameworkCore;
using PracticumTask.Database.Entities;
using PracticumTask.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Database
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PersonBook> PeopleBooks { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PersonBook>().HasKey(x => new { x.PersonId, x.BookId });

            var genres = new Genre[]
            {
                new Genre() { Id = 1, Name = "Приключения" },
                new Genre() { Id = 2, Name = "Фантастика" },
                new Genre() { Id = 3, Name = "Роман" }
            };

            builder.Entity<Genre>().HasData(genres);

            var authors = new Author[]
            {
                new Author()
                {
                    Id = 1,
                    FirstName = "Иван",
                    LastName = "Иванов",
                    MiddleName = "Иванович"
                },
                new Author()
                {
                    Id = 2,
                    FirstName = "Пётр",
                    LastName = "Петров"
                },
                new Author()
                {
                    Id = 3,
                    FirstName = "Тумба",
                    LastName = "Юмба"
                },
            };

            builder.Entity<Author>().HasData(authors);

            var books = new Book[]
            {
                new Book()
                {
                    Id = 1,
                    Title = "На волнах галоперидола",
                    AuthorId = authors[0].Id,
                },
                new Book()
                {
                    Id = 2,
                    Title = "Плачут ли программисты",
                    AuthorId = authors[1].Id,
                },
                new Book()
                {
                    Id = 3,
                    Title = "Подебажим?",
                    AuthorId = authors[2].Id,
                },
            };

            builder.Entity<Book>().HasData(books);

            var people = new Person[]
            {
                new Person()
                {
                    Id = 1,
                    FirstName = "Сергей",
                    LastName = "Драгун",
                    MiddleName = "Автоматов",
                    Birthdate = new DateTime(1943, 6, 2)
                },
                new Person()
                {
                    Id = 2,
                    FirstName = "Дмитрий",
                    LastName = "Пушкин",
                    Birthdate = new DateTime(1985, 3, 7)
                },
                new Person()
                {
                    Id = 3,
                    FirstName = "Александр",
                    LastName = "Чехов",
                    MiddleName = "Алексеевич",
                },
            };

            builder.Entity<Person>().HasData(people);

            var peopleBooks = new PersonBook[]
            {
                new PersonBook { PersonId = people[0].Id, BookId = books[0].Id },
                new PersonBook { PersonId = people[0].Id, BookId = books[1].Id },
                new PersonBook { PersonId = people[1].Id, BookId = books[1].Id },
                new PersonBook { PersonId = people[1].Id, BookId = books[2].Id }
            };

            builder.Entity<PersonBook>().HasData(peopleBooks);
        }

        void IApplicationContext.SaveChanges() => SaveChanges();
    }
}
