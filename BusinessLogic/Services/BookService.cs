using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticumTask.BusinessLogic.Services.Interfaces;
using PracticumTask.Database;
using PracticumTask.Database.Entities;
using PracticumTask.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.BusinessLogic.Services
{
    public class BookService : IBookService
    {
        private readonly IApplicationContext context;

        public BookService(IApplicationContext context) => this.context = context;

        public IEnumerable<Book> GetAll() => context.Books;

        public IEnumerable<Book> GetAllByGenre(string genreName)
            => context.Genres
                .Include(genre => genre.Books)
                .Where(genre => genre.Name == genreName)
                .SelectMany(genre => genre.Books);

        public Book Get(int id) => context.Books.Find(id);

        public IEnumerable<Book> GetAllByAuthor(string firstName, string lastName, string middleName)
            => context.Books.Where
            (
                x =>
                x.Author.FirstName == firstName &&
                x.Author.LastName == lastName &&
                x.Author.MiddleName == middleName
            );

        public Book GetByAuthorAndTitle
            (
                string firstName,
                string lastName,
                string middleName,
                string title
            )
            => context.Books.FirstOrDefault
            (
                x =>
                x.Title == title &&
                x.Author.FirstName == firstName &&
                x.Author.LastName == lastName &&
                x.Author.MiddleName == middleName
            );

        public Book GetByAuthorIdAndTitle(int authorId, string title)
            => context.Books.FirstOrDefault
            (
                x =>
                x.AuthorId == authorId &&
                x.Title == title
            );

        public Genre GetGenre(string name)
            => context.Genres.FirstOrDefault(x => x.Name == name);

        public bool IsAuthorExists([FromBody] Author value)
            => context.Authors.FirstOrDefault
                (
                    x =>
                    x.FirstName == value.FirstName &&
                    x.LastName == value.LastName &&
                    x.MiddleName == value.MiddleName
                ) != null;

        public bool IsBookTaken(int bookId)
            => context.PeopleBooks
                .FirstOrDefault(x => x.BookId == bookId) != null;

        public bool IsGenreExists(string genre)
            => context.Genres.FirstOrDefault(x => x.Name == genre) != null;

        public bool IsBookHasGenre([FromBody] Book value, string genreName)
            => value.Genres.Contains(GetGenre(genreName));

        public IEnumerable<Genre> GetAllGenres() => context.Genres;

        public void Add([FromBody] Book value)
        {
            context.Books.Add(value);
            context.SaveChanges();
        }

        public void AddAuthor([FromBody] Author value)
        {
            context.Authors.Add(value);
            context.SaveChanges();
        }
        public void AddGenre(string genreName)
        {
            context.Genres.Add(new Genre() { Name = genreName });
            context.SaveChanges();
        }

        public void AddGenreToBook([FromBody] Book book, string genre)
        { 
            book.Genres.Add(GetGenre(genre));
            context.SaveChanges();
        }

        public void DeleteGenreFromBook([FromBody] Book book, string genreName)
        { 
            book.Genres.Remove(GetGenre(genreName));
            context.SaveChanges();
        }

        public void Delete([FromBody] Book value)
        { 
            context.Books.Remove(value);
            context.SaveChanges();
        }
    }
}
