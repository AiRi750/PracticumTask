using Microsoft.AspNetCore.Mvc;
using PracticumTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Services
{
    interface IGenreService
    {
        IAsyncEnumerable<Genre> GetAll();
        Genre Get(int id);
        Genre Get(string title);
        public void Add([FromBody] Genre value);
        public void Delete([FromBody] Genre value);
        public void Save();
    }
}
