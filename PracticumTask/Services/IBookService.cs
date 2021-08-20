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
        public IEnumerable<Book> GetAll();
        public Book Get(int id);
        public IEnumerable<Book> GetAllByAuthor(string firstName, string lastName, string middleName);
        public Book GetByAuthorAndTitle
            (
                string firstName, 
                string lastName, 
                string middleName, 
                string title
            );
        public Book GetByAuthorIdAndTitle(int authorId, string title);
        public bool IsAuthorExists([FromBody] Author value);
        public bool IsBookTaken(int bookId);
        public IEnumerable<Genre> GetAllGenres();
        public void Add([FromBody] Book value);
        public void AddAuthor([FromBody] Author value);
        public void AddGenre([FromBody] Genre value);
        public void Delete([FromBody] Book value);
        public void Save();
    }
}
