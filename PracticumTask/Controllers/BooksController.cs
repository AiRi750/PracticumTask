using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticumTask.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PracticumTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationContext context;

        public BooksController(ApplicationContext context)
        {
            this.context = context;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public IAsyncEnumerable<Book> Get()
        {
            return context.Books;
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book value)
        {
            var book = context.Books.FirstOrDefault(x => x.Title == value.Title);
            if (book != null)
                return new ConflictResult();

            context.Add(value);
            context.SaveChanges();
            return Ok(context.Books);
        }

        // DELETE api/<AuthorsController>/5
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Author value, string title)
        {
            var book = context.Books
                .FirstOrDefault(x => x.Title == title && x.AuthorId == value.Id);
            if (book == null)
                return NotFound();

            context.Remove(book);
            context.SaveChanges();
            return Ok();
        }
    }
}
