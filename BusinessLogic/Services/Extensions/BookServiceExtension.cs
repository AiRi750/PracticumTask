using PracticumTask.BusinessLogic.Dto;
using PracticumTask.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTask.BusinessLogic.Services.Extensions
{
    public static class BookServiceExtension
    {
        public static IEnumerable<BookDto> ToDto(this IEnumerable<Book> books)
            => books.Select(x => x.ToDto());

        public static BookDto ToDto(this Book book)
            => new BookDto { Title = book.Title };
    }
}
