using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticumTask.Models;

namespace PracticumTask.Services
{
    public interface IAuthorService
    {
        public IQueryable<Author> GetAll();
        public IQueryable<Author> Get(string firstName);
        public Author Get(int id);
        public Author Get(string firstName, string lastName, string middleName);
        public void Add([FromBody] Author value);
        public void Delete([FromBody] Author value);
        public void Save();
    }
}
