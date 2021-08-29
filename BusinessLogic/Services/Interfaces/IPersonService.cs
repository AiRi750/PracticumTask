using Microsoft.AspNetCore.Mvc;
using PracticumTask.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.BusinessLogic.Services.Interfaces
{
    public interface IPersonService
    {
        public IEnumerable<Person> GetAll();
        public IEnumerable<Person> GetAll(string firstName, string lastName, string middleName);
        public IEnumerable<Book> GetAllBooks(int personId);
        public Book GetBook(int bookId);
        public Person Get(int id);
        public Person Get(string firstName, string lastName, string middleName);
        public PersonBook GetPersonBook(int personId, int bookId);
        public void Add([FromBody] Person value);
        public void AddBook([FromBody] PersonBook value);
        public void Delete([FromBody] Person value);
        public void DeletePersonBook([FromBody] PersonBook value);
        public void Save();
    }
}
