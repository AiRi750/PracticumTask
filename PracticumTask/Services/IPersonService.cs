using Microsoft.AspNetCore.Mvc;
using PracticumTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Services
{
    public interface IPersonService
    {
        public IEnumerable<Person> GetAll();
        public IEnumerable<Person> GetAll(string firstName, string lastName, string middleName);
        public IEnumerable<Book> GetAllBooks(int personId);
        public Person Get(int id);
        public Person Get(string firstName, string lastName, string middleName);
        public void Add([FromBody] Person value);
        public void Delete([FromBody] Person value);
        public void Save();
    }
}
