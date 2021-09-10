using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticumTask.Database.Entities;
using PracticumTask.BusinessLogic.Services.Interfaces;
using PracticumTask.BusinessLogic.Dto;
using PracticumTask.BusinessLogic.Services.Extensions;

namespace PracticumTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;

        public BooksController(IBookService service) => bookService = service;

        [HttpGet]
        public IEnumerable<BookDto> GetAll() => bookService.GetAll().ToDto();

        [HttpGet("booksByGenre")]
        public IEnumerable<BookDto> GetAllByGenre(string genreName)
            => bookService.GetAllByGenre(genreName).ToDto();

        [HttpGet("booksByAuthor")]
        public IEnumerable<BookDto> GetAllByAuthor(string firstName, string lastName, string middleName)
            => bookService.GetAllByAuthor(firstName, lastName, middleName).ToDto();

        [HttpPut("addGenre")]
        public async Task<IActionResult> AddGenre([FromBody] Book value, string genreName)
        {
            var book = bookService.GetByAuthorAndTitle
                (
                    value.Author.FirstName,
                    value.Author.LastName,
                    value.Author.MiddleName,
                    value.Title
                );
            if (book == null)
                return NotFound();
            if (!bookService.IsGenreExists(genreName))
                bookService.AddGenre(genreName);
            if (bookService.IsBookHasGenre(book, genreName))
                return Conflict();

            bookService.AddGenreToBook(book, genreName);
            return Ok();
        }

        [HttpPut("deleteGenre")]
        public async Task<IActionResult> DeleteGenre([FromBody] Book value, string genreName)
        {
            var book = bookService.GetByAuthorAndTitle
                (
                    value.Author.FirstName,
                    value.Author.LastName,
                    value.Author.MiddleName,
                    value.Title
                );
            if (book == null || !bookService.IsBookHasGenre(book, genreName))
                return NotFound();

            bookService.DeleteGenreFromBook(book, genreName);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int bookId)
        {
            var book = bookService.Get(bookId);
            if (book == null)
                return NotFound();
            if (bookService.IsBookTaken(bookId))
                return BadRequest("Ошибка - книга у пользователя");

            bookService.Delete(book);
            return Ok();
        }
    }
}
