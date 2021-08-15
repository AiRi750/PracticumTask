using Microsoft.AspNetCore.Mvc;
using PracticumTask.Models;
using PracticumTask.Services;
using PracticumTask.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService personService;

        public PeopleController(IPersonService service) => personService = service;

        [HttpGet]
        public IQueryable<PersonDto> GetAll() => personService.GetAll().ToDto();

        [HttpGet("byFullName")]
        public async Task<IActionResult> Get(string firstName, string lastName, string middleName)
        {
            var person = personService.Get(firstName, lastName, middleName);
            if (person == null)
                return NotFound();
            return Ok(person.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person value)
        {
            var person = personService.Get
            (
                value.FirstName,
                value.LastName,
                value.MiddleName
            );
            if (person != null)
                return Conflict();

            personService.Add(value);
            personService.Save();
            return Ok(personService.GetAll().ToDto());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string firstName, string lastName, string middleName)
        {
            var person = personService.Get(firstName, lastName, middleName);
            if (person == null)
                return NotFound();

            personService.Delete(person);
            personService.Save();
            return Ok();
        }
    }
}
