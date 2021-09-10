using Microsoft.AspNetCore.Mvc;
using PracticumTask.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.BusinessLogic.Services.Interfaces
{
    public interface IBookService
    {
        public IEnumerable<Book> GetAll();
        public IEnumerable<Book> GetAllByAuthor(string firstName, string lastName, string middleName);
        public IEnumerable<Book> GetAllByGenre(string genreName);
        public Book Get(int id);
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
        public bool IsGenreExists(string genreName);
        public bool IsBookHasGenre([FromBody] Book value, string genreName);
        public IEnumerable<Genre> GetAllGenres();
        public void Add([FromBody] Book value);
        public void AddAuthor([FromBody] Author value);
        public void AddGenre(string genreName);
        public void AddGenreToBook([FromBody] Book value, string genreName);
        public void DeleteGenreFromBook([FromBody] Book value, string genreName);
        public void Delete([FromBody] Book value);
    }
}
