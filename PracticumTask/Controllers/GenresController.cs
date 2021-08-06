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
    public class GenresController : ControllerBase
    {
        private readonly ApplicationContext context;

        public GenresController(ApplicationContext context)
        {
            this.context = context;
        }

        // GET: api/<GenresController>
        [HttpGet]
        public IAsyncEnumerable<Genre> Get()
        {
            return context.Genres;
        }

        // GET api/<GenresController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var genre = await context.Genres.FindAsync(id);
            if (genre == null)
                return NotFound();
            return Ok(genre);
        }

        // POST api/<GenresController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Genre value)
        {
            var genre = context.Genres.FirstOrDefault(x => x.Name == value.Name); //todo - refactor to async?
            if (genre != null)
                return new ConflictResult();
            
            context.Add(value);
            context.SaveChanges();
            return Ok(context.Genres);
        }

        // DELETE api/<GenresController>/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var genre = context.Genres.FirstOrDefault(x => x.Name == name);
            if (genre == null)
                return NotFound();

            context.Remove(genre);
            context.SaveChanges();
            return Ok();
        }
    }
}
