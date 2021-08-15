using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracticumTask.Models
{
    public class PersonDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime? Birthdate { get; set; }
    }
}
