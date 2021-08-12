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
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationContext context;

        public AuthorsController(ApplicationContext context)
        {
            this.context = context;
        }

        // GET: api/<AuthorsController>
        [HttpGet]
        public IAsyncEnumerable<Author> Get()
        {
            return context.Authors;
        }

        // GET api/<AuthorsController>/5
        [HttpGet("{firstName}")]
        public async Task<IActionResult> Get(string firstName)
        {
            var author = context.Authors.Where(x => x.FirstName == firstName);
            if (author.FirstOrDefault() == null)
                return NotFound();
            return Ok(author);
        }

        // POST api/<AuthorsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Author value)
        {
            var author = context.Authors
                .FirstOrDefault(x => x.FirstName == value.FirstName
                    && x.LastName == value.LastName
                    && x.MiddleName == value.MiddleName);
            if (author != null)
                return new ConflictResult();

            context.Add(value);
            context.SaveChanges();
            return Ok(context.Authors);
        }

        // DELETE api/<AuthorsController>/5
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Author value)
        {
            var author = context.Authors
                .FirstOrDefault(x => x.FirstName == value.FirstName
                    && x.LastName == value.LastName
                    && x.MiddleName == value.MiddleName);
            if (author == null)
                return NotFound();

            context.Remove(author);
            context.SaveChanges();
            return Ok();
        }
    }
}
