using Microsoft.AspNetCore.Mvc;
using PracticumTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Services
{
    public class PersonService : IPersonService
    {
        private readonly ApplicationContext context;

        public PersonService(ApplicationContext context) => this.context = context;

        public IQueryable<Person> GetAll() => context.People;

        public IQueryable<Person> GetAll(string firstName, string lastName, string middleName)
            => context.People.Where
            (
                x =>
                x.FirstName == firstName &&
                x.LastName == lastName &&
                x.MiddleName == middleName
            );

        public Person Get(int id) => context.People.Find(id);

        public Person Get(string firstName, string lastName, string middleName)
            => context.People.FirstOrDefault
            (
                x =>
                x.FirstName == firstName &&
                x.LastName == lastName &&
                x.MiddleName == middleName
            );

        public void Add([FromBody] Person value) => context.Add(value);

        public void Delete([FromBody] Person value) => context.Remove(value);

        public void Save() => context.SaveChanges();
    }
}
