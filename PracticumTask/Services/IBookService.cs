using Microsoft.AspNetCore.Mvc;
using PracticumTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Services
{
    interface IBookService
    {
        IAsyncEnumerable<Book> GetAll();
        Book Get(int id);
        IQueryable<Book> GetByAuthor(string firstName, string lastName, string middleName);
        public void Add([FromBody] Book value);
        public void Delete([FromBody] Author author, string name);
        public void Save();
    }
}
