using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticumTask.BusinessLogic.Services.Interfaces;
using PracticumTask.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.BusinessLogic.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationContext context;

        public AuthorService(ApplicationContext context) => this.context = context;

        public IEnumerable<Author> GetAll() => context.Authors;

        public IEnumerable<Author> Get(string firstName)
            => context.Authors.Where(x => x.FirstName == firstName);

        public Author Get(int id) => context.Authors.Find(id);

        public Author Get(string firstName, string lastName, string middleName)
            => context.Authors.FirstOrDefault
            (
                x =>
                x.FirstName == firstName &&
                x.LastName == lastName &&
                x.MiddleName == middleName
            );

        public void Add([FromBody] Author value) => context.Add(value);

        public void Delete([FromBody] Author value) => context.Remove(value);

        public void Save() => context.SaveChanges();
    }
}
