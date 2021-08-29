using PracticumTask.BusinessLogic.Dto;
using PracticumTask.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.BusinessLogic.Services.Extensions
{
    public static class GenreServiceExtension
    {
        public static IEnumerable<GenreDto> ToDto(this IEnumerable<Genre> genres)
            => genres.Select(x => x.ToDto());

        public static GenreDto ToDto(this Genre genre)
            => new GenreDto { Name = genre.Name };
    }
}
