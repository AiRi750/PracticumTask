using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Models
{
    public class PersonBook
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
