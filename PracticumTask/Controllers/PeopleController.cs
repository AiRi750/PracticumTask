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
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService personService;

        public PeopleController(IPersonService service) => personService = service;

        [HttpGet]
        public IEnumerable<PersonDto> GetAll() => personService.GetAll().ToDto();

        [HttpGet("personBooks")]
        public IEnumerable<BookDto> GetAllBooks(int personId) => personService.GetAllBooks(personId).ToDto();

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
            return Ok(value.ToDto());
        }

        [HttpPost]
        [Route("AddPersonBook")]
        public async Task<IActionResult> AddPersonBook([FromBody] PersonBook value)
        {
            if (personService.Get(value.PersonId) == null ||
                personService.GetBook(value.BookId) == null)
                return NotFound();
            if (personService.GetPersonBook(value.PersonId, value.BookId) != null)
                return Conflict();

            personService.AddBook(value);
            return Ok();
        }

        [HttpPost]
        [Route("UpdatePerson")]
        public async Task<IActionResult> Post
            (
                [FromBody] Person oldValue,
                string firstName,
                string lastName,
                string middleName,
                DateTime? birthdate
            )
        {
            var oldPerson = personService.Get
            (
                oldValue.FirstName,
                oldValue.LastName,
                oldValue.MiddleName
            );
            if (oldPerson == null)
                return NotFound();

            personService.UpdatePerson(oldPerson, firstName, lastName, middleName, birthdate);
            return Ok(oldPerson.ToDto());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string firstName, string lastName, string middleName)
        {
            var people = personService.GetAll(firstName, lastName, middleName);
            if (people.FirstOrDefault() == null)
                return NotFound();

            foreach (var person in people)
                personService.Delete(person);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = personService.Get(id);
            if (person == null)
                return NotFound();

            personService.Delete(person);
            return Ok();
        }

        [HttpDelete("RemovePersonBook")]
        public async Task<IActionResult> Delete([FromBody] PersonBook value)
        {
            var personBook = personService.GetPersonBook(value.PersonId, value.BookId);
            if (personBook == null)
                return NotFound();

            personService.DeletePersonBook(personBook);
            return Ok();
        }
    }
}
