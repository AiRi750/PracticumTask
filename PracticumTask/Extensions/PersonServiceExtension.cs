using PracticumTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Extensions
{
    public static class PersonServiceExtension
    {
        public static IEnumerable<PersonDto> ToDto(this IEnumerable<Person> people)
            => people.Select(x => x.ToDto());

        public static PersonDto ToDto(this Person person)
            => new PersonDto
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                MiddleName = person.MiddleName,
                Birthdate = person.Birthdate
            };
    }
}
