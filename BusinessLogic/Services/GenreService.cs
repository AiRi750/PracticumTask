﻿using Microsoft.AspNetCore.Mvc;
using PracticumTask.BusinessLogic.Services.Interfaces;
using PracticumTask.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.BusinessLogic.Services
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationContext context;

        public GenreService(ApplicationContext context) => this.context = context;

        public IEnumerable<Genre> GetAll() => context.Genres;

        public Genre Get(string name) => context.Genres.FirstOrDefault(x => x.Name == name);

        public void Add([FromBody] Genre value) => context.Add(value);

        public void Delete([FromBody] Genre value) => context.Remove(value);

        public void Save() => context.SaveChanges();
    }
}