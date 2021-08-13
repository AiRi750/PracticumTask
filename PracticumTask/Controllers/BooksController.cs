using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticumTask.Models;
using PracticumTask.Services;

namespace PracticumTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;

        public BooksController(IBookService service) => bookService = service;

        [HttpGet]
        public IAsyncEnumerable<Book> Get() => bookService.GetAll();

        [HttpGet("{authorId}")]
        public async Task<IActionResult> Get(int id)
        {
            var books = bookService.Get(id);
            if (books == null)
                return NotFound();
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book value)
        {
            var book = bookService.GetByAuthorAndTitle(
                value.Author.FirstName, value.Author.LastName, value.Author.MiddleName, value.Title);
            if (book != null)
                return Conflict();

            bookService.Add(value);
            bookService.Save();
            return Ok(bookService.GetAll());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int authorId, string title)
        {
            var book = bookService.GetByAuthorIdAndTitle(authorId, title);
            if (book == null)
                return NotFound();

            bookService.Delete(book);
            bookService.Save();
            return Ok();
        }
    }
}
