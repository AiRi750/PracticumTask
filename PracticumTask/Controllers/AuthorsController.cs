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
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService authorService;

        public AuthorsController(IAuthorService service) => authorService = service;

        [HttpGet]
        public IAsyncEnumerable<Author> GetAll() => authorService.GetAll();

        [HttpGet("{firstName}")]
        public async Task<IActionResult> Get(string firstName)
        {
            var authors = authorService.Get(firstName);
            if (authors.FirstOrDefault() == null)
                return NotFound();
            return Ok(authors);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Author value)
        {
            var author = authorService.Get(value.FirstName, value.LastName, value.MiddleName);
            if (author != null)
                return Conflict();

            authorService.Add(value);
            authorService.Save();
            return Ok(authorService.GetAll());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string firstName, string lastName, string middleName)
        {
            var author = authorService.Get(firstName, lastName, middleName);
            if (author == null)
                return NotFound();

            authorService.Delete(author);
            authorService.Save();
            return Ok();
        }
    }
}
