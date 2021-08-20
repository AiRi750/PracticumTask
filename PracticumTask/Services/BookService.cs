using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticumTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationContext context;

        public BookService(ApplicationContext context) => this.context = context;

        public IEnumerable<Book> GetAll() => context.Books;

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

        public IEnumerable<Genre> GetAllGenres() => context.Genres;

        public void Add([FromBody] Book value) => context.Add(value);

        public void AddAuthor([FromBody] Author value) => context.Add(value);

        public void AddGenre([FromBody] Genre value) => context.Add(value);

        public void Delete([FromBody] Book value) 
            => context.Remove(value);

        public void Save() => context.SaveChanges();
    }
}
