using Microsoft.EntityFrameworkCore;
using PracticumTask.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticumTask.Database.Interfaces
{
    public interface IApplicationContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PersonBook> PeopleBooks { get; set; }
    }
}
