using PracticumTask.Models;
using PracticumTask.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Extensions
{
    public static class GenreServiceExtension
    {
        public static IQueryable<GenreDto> ToDto(this IQueryable<Genre> genres)
            => genres.Select(x => x.ToDto());

        public static GenreDto ToDto(this Genre genre)
            => new GenreDto { Name = genre.Name };
    }
}
