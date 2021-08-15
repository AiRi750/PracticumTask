using PracticumTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Extensions
{
    public static class AuthorServiceExtension
    {
        public static IQueryable<AuthorDto> ToDto(this IQueryable<Author> authors)
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
