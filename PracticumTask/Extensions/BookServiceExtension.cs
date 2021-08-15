using PracticumTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.Extensions
{
    public static class BookServiceExtension
    {
        public static IQueryable<BookDto> ToDto(this IQueryable<Book> books)
            => books.Select(x => x.ToDto());

        public static BookDto ToDto(this Book book)
            => new BookDto { Title = book.Title };
    }
}
