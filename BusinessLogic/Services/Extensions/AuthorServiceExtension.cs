using PracticumTask.BusinessLogic.Dto;
using PracticumTask.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.BusinessLogic.Services.Extensions
{
    public static class AuthorServiceExtension
    {
        public static IEnumerable<AuthorDto> ToDto(this IEnumerable<Author> authors)
            => authors.Select(x => x.ToDto());

        public static AuthorDto ToDto(this Author author)
            => new AuthorDto
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                MiddleName = author.MiddleName
            };
    }
}
