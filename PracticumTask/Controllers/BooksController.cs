using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticumTask.Models;
using PracticumTask.Services;
using PracticumTask.Extensions;

namespace PracticumTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;

        public BooksController(IBookService service) => bookService = service;

        [HttpGet]
        public IQueryable<BookDto> GetAll() => bookService.GetAll().ToDto();

        [HttpGet("{authorId}")]
        public async Task<IActionResult> Get(int authorId)
        {
            var book = bookService.Get(authorId);
            if (book == null)
                return NotFound();
            return Ok(book.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book value)
        {
            var book = bookService.GetByAuthorAndTitle
                (
                    value.Author.FirstName, 
                    value.Author.LastName, 
                    value.Author.MiddleName, 
                    value.Title
                );
            if (book != null)
                return Conflict();

            bookService.Add(value);
            bookService.Save();
            return Ok(bookService.GetAll().ToDto());
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
