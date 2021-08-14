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
            => authors.Select(x => 
                new AuthorDto 
                { 
                    FirstName = x.FirstName, 
                    LastName = x.LastName, 
                    MiddleName = x.MiddleName 
                });

        public static AuthorDto ToDto(this Author author)
            => new AuthorDto 
            { 
                FirstName = author.FirstName,
                LastName = author.LastName,
                MiddleName = author.MiddleName
            };
    }
}
