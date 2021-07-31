using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
