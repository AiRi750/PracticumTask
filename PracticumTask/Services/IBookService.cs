using Microsoft.AspNetCore.Mvc;
using PracticumTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Services
{
    public interface IBookService
    {
        public IQueryable<Book> GetAll();
        public Book Get(int id);
        public IQueryable<Book> GetAllByAuthor(string firstName, string lastName, string middleName);
        public Book GetByAuthorAndTitle
            (
                string firstName, 
                string lastName, 
                string middleName, 
                string title
            );
        public Book GetByAuthorIdAndTitle(int authorId, string title);
        public void Add([FromBody] Book value);
        public void Delete([FromBody] Book value);
        public void Save();
    }
}
