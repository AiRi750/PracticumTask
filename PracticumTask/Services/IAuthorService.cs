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
        IAsyncEnumerable<Author> GetAll();
        IQueryable<Author> Get(string firstName);
        Author Get(int id);
        Author Get(string firstName, string lastName, string middleName);
        public void Add([FromBody] Author value);
        public void Delete([FromBody] Author value);
        public void Save();
    }
}
