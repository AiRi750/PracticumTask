﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticumTask.Models;
using PracticumTask.Services;
using PracticumTask.Extensions;

namespace PracticumTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService genreService;

        public GenresController(IGenreService service) => genreService = service;

        [HttpGet]
        public IEnumerable<GenreDto> Get() => genreService.GetAll().ToDto();

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var genre = genreService.Get(name);
            if (genre == null)
                return NotFound();
            return Ok(genre.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Genre value)
        {
            var genre = genreService.Get(value.Name);
            if (genre != null)
                return Conflict();

            genreService.Add(value);
            genreService.Save();
            return Ok(genreService.GetAll().ToDto());
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var genre = genreService.Get(name);
            if (genre == null)
                return NotFound();

            genreService.Delete(genre);
            genreService.Save();
            return Ok();
        }
    }
}
