using Microsoft.AspNetCore.Mvc;
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

        public void Add([FromBody] Book value) => context.Add(value);

        public void Delete([FromBody] Book value) 
            => context.Remove(value);

        public void Save() => context.SaveChanges();
    }
}
