using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PracticumTask.BusinessLogic.Dto;
using PracticumTask.BusinessLogic.Services.Interfaces;
using PracticumTask.Controllers;
using PracticumTask.Database.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Tests
{
    [TestFixture]
    class BooksControllerTests
    {
        [Test]
        public void GetAllBooks()
        {
            var mock = new Mock<IBookService>();
            mock.Setup(service => service.GetAll()).Returns(GetTestBooks());
            var controller = new BooksController(mock.Object);

            var result = controller.GetAll();

            mock.Verify(service => service.GetAll());
            Assert.AreEqual(GetTestBooks().Count, result.Count());
        }

        [Test]
        [TestCase("genre2")]
        public void GetAllBooksByGenre(string genreName)
        {
            var mock = new Mock<IBookService>();
            mock.Setup(service => service.GetAllByGenre(genreName))
                .Returns(GetTestBooks()
                    .Where(b => b.Genres.Where(g => g.Name == genreName)
                    .Any()));
            var controller = new BooksController(mock.Object);

            var result = controller.GetAllByGenre(genreName);

            mock.Verify(service => service.GetAllByGenre(genreName));
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        [TestCase("second", "second", null)]
        public void GetAllBooksByAuthor(string firstName, string lastName, string middleName)
        {
            var mock = new Mock<IBookService>();
            mock.Setup(service => service.GetAllByAuthor(firstName, lastName, middleName))
                .Returns(GetTestBooks()
                    .Where
                    (
                        b => 
                        b.Author.FirstName == firstName && 
                        b.Author.LastName == lastName && 
                        b.Author.MiddleName == middleName
                    ));
            var controller = new BooksController(mock.Object);

            var result = controller.GetAllByAuthor(firstName, lastName, middleName);

            mock.Verify(service => service.GetAllByAuthor(firstName, lastName, middleName));
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        [TestCase("genre3")]
        public void AddGenreToNotExistingBook(string genreName)
        {
            var book = GetTestBooks()[1];
            var author = GetTestBooks()[2].Author;
            var mock = new Mock<IBookService>();
            mock.Setup(service => service.GetByAuthorAndTitle
                (
                    author.FirstName, 
                    author.LastName, 
                    author.MiddleName, 
                    book.Title
                ));
            var controller = new BooksController(mock.Object);

            var result = controller.AddGenre(book, genreName);

            mock.Verify(service => service.AddGenreToBook(book, genreName), Times.Never);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        [TestCase(1, "genre5")]
        public void AddNotExistingGenreToBook(int bookId, string genreName)
        {
            var book = GetTestBooks().FirstOrDefault(b => b.Id == bookId);
            var mock = new Mock<IBookService>();
            mock.Setup(service => service.GetByAuthorAndTitle
                (
                    book.Author.FirstName,
                    book.Author.LastName,
                    book.Author.MiddleName,
                    book.Title
                ))
                .Returns(book);
            mock.Setup(service => service.IsGenreExists(genreName))
                .Returns(false);
            mock.Setup(service => service.AddGenre(genreName));
            var controller = new BooksController(mock.Object);

            var result = controller.AddGenre(book, genreName);

            mock.Verify(service => service.AddGenre(genreName), Times.Once);
            mock.Verify(service => service.AddGenreToBook(book, genreName));
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        [TestCase(1, "genre1")]
        public void AddExistingGenreToBook(int bookId, string genreName)
        {
            var book = GetTestBooks().FirstOrDefault(b => b.Id == bookId);
            var mock = new Mock<IBookService>();
            mock.Setup(service => service.GetByAuthorAndTitle
                (
                    book.Author.FirstName,
                    book.Author.LastName,
                    book.Author.MiddleName,
                    book.Title
                ))
                .Returns(book);
            mock.Setup(service => service.IsGenreExists(genreName))
                .Returns(true);
            mock.Setup(service => service.IsBookHasGenre(book, genreName))
                .Returns(true);
            var controller = new BooksController(mock.Object);

            var result = controller.AddGenre(book, genreName);

            mock.Verify(service => service.AddGenreToBook(book, genreName), Times.Never);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<ConflictResult>(result.Result);
        }

        [Test]
        [TestCase(1, "genre1")]
        public void DeleteExistingGenre(int bookId, string genreName)
        {
            var book = GetTestBooks().FirstOrDefault(b => b.Id == bookId);
            var mock = new Mock<IBookService>();
            mock.Setup(service => service.GetByAuthorAndTitle
                (
                    book.Author.FirstName,
                    book.Author.LastName,
                    book.Author.MiddleName,
                    book.Title
                ))
                .Returns(book);
            mock.Setup(service => service.IsBookHasGenre(book, genreName))
                .Returns(true);
            var controller = new BooksController(mock.Object);

            var result = controller.DeleteGenre(book, genreName);

            mock.Verify(service => service.DeleteGenreFromBook(book, genreName));
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        [TestCase(1, "genre5")]
        public void DeleteNotExistingGenre(int bookId, string genreName)
        {
            var book = GetTestBooks().FirstOrDefault(b => b.Id == bookId);
            var mock = new Mock<IBookService>();
            mock.Setup(service => service.GetByAuthorAndTitle
                (
                    book.Author.FirstName,
                    book.Author.LastName,
                    book.Author.MiddleName,
                    book.Title
                ))
                .Returns(book);
            mock.Setup(service => service.IsBookHasGenre(book, genreName))
                .Returns(false);
            var controller = new BooksController(mock.Object);

            var result = controller.DeleteGenre(book, genreName);

            mock.Verify(service => service.DeleteGenreFromBook(book, genreName), Times.Never);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        [TestCase(2)]
        public void DeleteExistingBook(int bookId)
        {
            var book = GetTestBooks().FirstOrDefault(b => b.Id == bookId);
            var mock = new Mock<IBookService>();
            mock.Setup(service => service.Get(bookId))
                .Returns(book);
            var controller = new BooksController(mock.Object);

            var result = controller.Delete(bookId);

            mock.Verify(service => service.Delete(book));
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        public void DeleteNotExistingBook()
        {
            var books = GetTestBooks();
            var book = new Book() 
            { 
                Id = 5, 
                Title = "test", 
                Author = books[0].Author, 
                AuthorId = books[0].AuthorId 
            };
            var mock = new Mock<IBookService>();
            mock.Setup(service => service.Get(book.Id));
            var controller = new BooksController(mock.Object);

            var result = controller.Delete(book.Id);

            mock.Verify(service => service.Delete(book), Times.Never);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        private List<Book> GetTestBooks()
        {
            var genres = new List<Genre>()
            {
                new Genre(){ Id = 1, Name = "genre1" },
                new Genre(){ Id = 2, Name = "genre2" },
                new Genre(){ Id = 3, Name = "genre3" },
                new Genre(){ Id = 4, Name = "genre4" },
            };

            var authors = new List<Author>()
            {
                new Author(){ Id = 1, FirstName = "first", LastName = "first", MiddleName = "first" },
                new Author(){ Id = 2, FirstName = "second", LastName = "second" },
            };

            var people = new List<Person>()
            {
                new Person() { Id = 1, FirstName = "person1", LastName = "person1" },
                new Person() { Id = 2, FirstName = "person2", LastName = "person2" },
            };

            var books = new List<Book>()
            {
                new Book() 
                { 
                    Id = 1, 
                    Title = "book1", 
                    Author = authors[0], 
                    AuthorId = authors[0].Id, 
                    Genres = new List<Genre>() { genres[0], genres[1] },
                },
                new Book()
                {
                    Id = 2,
                    Title = "book2",
                    Author = authors[0],
                    AuthorId = authors[0].Id,
                    Genres = new List<Genre>() { genres[1], genres[2] }
                },
                new Book()
                {
                    Id = 3,
                    Title = "book3",
                    Author = authors[1],
                    AuthorId = authors[1].Id,
                    Genres = new List<Genre>() { genres[2], genres[3] }
                }
            };

            var peopleBooks = new List<PersonBook>()
            {
                new PersonBook() { Book = books[0], BookId = books[0].Id, Person = people[1], PersonId = people[1].Id },
                new PersonBook() { Book = books[1], BookId = books[1].Id, Person = people[0], PersonId = people[0].Id },
                new PersonBook() { Book = books[2], BookId = books[2].Id, Person = people[0], PersonId = people[0].Id },
            };

            return books;
        }
    }
}
