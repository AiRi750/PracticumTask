using Microsoft.AspNetCore.Mvc;
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
    public class GenreService : IGenreService
    {
        private readonly IApplicationContext context;

        public GenreService(IApplicationContext context) => this.context = context;

        public IEnumerable<Genre> GetAll() => context.Genres;

        public Genre Get(string name) => context.Genres.FirstOrDefault(x => x.Name == name);

        public void Add([FromBody] Genre value)
        {
            context.Genres.Add(value);
            context.SaveChanges();
        }

        public void Delete([FromBody] Genre value)
        {
            context.Genres.Remove(value);
            context.SaveChanges();
        }
    }
}
