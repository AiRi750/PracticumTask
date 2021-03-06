using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracticumTask.Database.Entities
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime? Birthdate { get; set; }

        public List<PersonBook> PeopleBooks { get; set; } = new List<PersonBook>();
    }
}
