using Microsoft.AspNetCore.Mvc;
using PracticumTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Services
{
    interface IPersonService
    {
        IAsyncEnumerable<Person> GetAll();
        Person Get(int id);
        Person Get(string firstName, string lastName, string middleName);
        public void Add([FromBody] Person value);
        public void Delete([FromBody] Person value);
        public void Save();
    }
}
