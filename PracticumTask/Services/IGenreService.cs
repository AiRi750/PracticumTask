using Microsoft.AspNetCore.Mvc;
using PracticumTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Services
{
    public interface IGenreService
    {
        public IEnumerable<Genre> GetAll();
        public Genre Get(string name);
        public void Add([FromBody] Genre value);
        public void Delete([FromBody] Genre value);
        public void Save();
    }
}
