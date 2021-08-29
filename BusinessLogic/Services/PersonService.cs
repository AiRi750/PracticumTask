using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticumTask.BusinessLogic.Services.Interfaces;
using PracticumTask.Database;
using PracticumTask.Database.Entities;
using PracticumTask.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.BusinessLogic.Services
{
    public class PersonService : IPersonService
    {
        private readonly IApplicationContext context;

        public PersonService(IApplicationContext context) => this.context = context;

        public IEnumerable<Person> GetAll() => context.People;

        public IEnumerable<Person> GetAll(string firstName, string lastName, string middleName)
            => context.People.Where
            (
                x =>
                x.FirstName == firstName &&
                x.LastName == lastName &&
                x.MiddleName == middleName
            );

        public IEnumerable<Book> GetAllBooks(int personId)
            => context.PeopleBooks
                .Where(pb => pb.PersonId == personId)
                .Select(pb => pb.Book);

        public Book GetBook(int bookId) => context.Books.Find(bookId);

        public PersonBook GetPersonBook(int personId, int bookId)
            => context.PeopleBooks.Find(personId, bookId);

        public Person Get(int id) => context.People.Find(id);

        public Person Get(string firstName, string lastName, string middleName)
            => context.People.FirstOrDefault
            (
                x =>
                x.FirstName == firstName &&
                x.LastName == lastName &&
                x.MiddleName == middleName
            );

        public void Add([FromBody] Person value) => context.People.Add(value);

        public void AddBook([FromBody] PersonBook value)
            => context.PeopleBooks.Add(value);

        public void Delete([FromBody] Person value) => context.People.Remove(value);

        public void DeletePersonBook([FromBody] PersonBook value) => context.PeopleBooks.Remove(value);

        public void Save() => context.SaveChanges();
    }
}
