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

        public IAsyncEnumerable<Book> GetAll() => context.Books;

        public Book Get(int id) => context.Books.Find(id);

        public IQueryable<Book> GetByAuthor(string firstName, string lastName, string middleName) 
            => context.Books.Where(x =>
                x.Author.FirstName == firstName &&
                x.Author.LastName == lastName &&
                x.Author.MiddleName == middleName);

        public void Add([FromBody] Book value) => context.Add(value);

        public void Delete([FromBody] Author author, string title) 
            => context.Remove(
                GetByAuthor(author.FirstName, author.LastName, author.MiddleName)
                    .First(x => x.Title == title));

        public void Save() => context.SaveChanges();
    }
}
