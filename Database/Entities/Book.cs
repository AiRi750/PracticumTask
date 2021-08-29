using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Database.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public Author Author { get; set; }
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<PersonBook> PeopleBooks { get; set; } = new List<PersonBook>();
    }
}
