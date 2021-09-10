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
    class PeopleControllerTests
    {
        [Test]
        public void GetAllPeople()
        {
            var mock = new Mock<IPersonService>();
            mock.Setup(service => service.GetAll()).Returns(GetTestPeople());
            var controller = new PeopleController(mock.Object);

            var result = controller.GetAll();

            mock.Verify(service => service.GetAll());
            Assert.AreEqual(GetTestPeople().Count, result.Count());
        }

        [Test]
        [TestCase(1)]
        public void GetAllBooks(int personId)
        {
            var mock = new Mock<IPersonService>();
            mock.Setup(service => service.GetAllBooks(personId))
                .Returns(GetTestPeople()
                    .FirstOrDefault(p => p.Id == personId)
                    .PeopleBooks
                    .Select(pb => pb.Book));
            var controller = new PeopleController(mock.Object);

            var result = controller.GetAllBooks(personId);

            mock.Verify(service => service.GetAllBooks(personId));
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        [TestCase("person2", "person2", null)]
        public void GetExistingPerson(string firstName, string lastName, string middleName)
        {
            var mock = new Mock<IPersonService>();
            mock.Setup(service => service.Get(firstName, lastName, middleName))
                .Returns(GetTestPeople()
                    .FirstOrDefault
                    (
                        p => 
                        p.FirstName == firstName && 
                        p.LastName == lastName && 
                        p.MiddleName == middleName
                    ));
            var controller = new PeopleController(mock.Object);

            var result = controller.Get(firstName, lastName, middleName);

            mock.Verify(service => service.Get(firstName, lastName, middleName));
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        [TestCase("person3", "person3", null)]
        public void GetNotExistingPerson(string firstName, string lastName, string middleName)
        {
            var mock = new Mock<IPersonService>();
            mock.Setup(service => service.Get(firstName, lastName, middleName))
                .Returns(GetTestPeople()
                    .FirstOrDefault
                    (
                        p =>
                        p.FirstName == firstName &&
                        p.LastName == lastName &&
                        p.MiddleName == middleName
                    ));
            var controller = new PeopleController(mock.Object);

            var result = controller.Get(firstName, lastName, middleName);

            mock.Verify(service => service.Get(firstName, lastName, middleName));
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        [TestCase(1)]
        public void AddExistingPerson(int personId)
        {
            var person = GetTestPeople().FirstOrDefault(p => p.Id == personId);
            var mock = new Mock<IPersonService>();
            mock.Setup(service => service.Get(person.FirstName, person.LastName, person.MiddleName))
                .Returns(GetTestPeople()
                    .FirstOrDefault
                    (
                        p =>
                        p.FirstName == person.FirstName &&
                        p.LastName == person.LastName &&
                        p.MiddleName == person.MiddleName
                    ));
            mock.Setup(service => service.Add(person));
            var controller = new PeopleController(mock.Object);

            var result = controller.Post(person);

            mock.Verify(service => service.Add(person), Times.Never);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<ConflictResult>(result.Result);
        }

        [Test]
        [TestCase(3, "test", "test", null)]
        public void AddNotExistingPerson(int id, string firstName, string lastName, string middleName)
        {
            var person = new Person() 
            { 
                Id = id, 
                FirstName = firstName, 
                LastName = lastName, 
                MiddleName = middleName };
            var mock = new Mock<IPersonService>();
            mock.Setup(service => service.Get(person.FirstName, person.LastName, person.MiddleName))
                .Returns(GetTestPeople()
                    .FirstOrDefault
                    (
                        p =>
                        p.FirstName == person.FirstName &&
                        p.LastName == person.LastName &&
                        p.MiddleName == person.MiddleName
                    ));
            mock.Setup(service => service.Add(person));
            var controller = new PeopleController(mock.Object);

            var result = controller.Post(person);

            mock.Verify(service => service.Add(person));
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        [TestCase(2, 1, 1)]
        public void AddBookToPerson(int otherPersonId, int personId, int bookId)
        {
            var person = GetTestPeople().FirstOrDefault(p => p.Id == personId);
            var book = GetTestPeople()
                .FirstOrDefault(p => p.Id == otherPersonId)
                .PeopleBooks
                .FirstOrDefault(b => b.BookId == bookId).Book;
            var personBook = new PersonBook() 
                { 
                    BookId = book.Id, 
                    Book = book, 
                    PersonId = person.Id, 
                    Person = person 
                };
            var mock = new Mock<IPersonService>();
            mock.Setup(service => service.Get(personId))
                .Returns(person);
            mock.Setup(service => service.GetBook(bookId))
                .Returns(book);
            var controller = new PeopleController(mock.Object);

            var result = controller.AddPersonBook(personBook);

            mock.Verify(service => service.AddBook(personBook));
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        [TestCase(1)]
        public void AddNotExistingBookToPerson(int personId)
        {
            var person = GetTestPeople().FirstOrDefault(p => p.Id == personId);
            var book = person.PeopleBooks.FirstOrDefault().Book;
            var personBook = new PersonBook()
            {
                BookId = book.Id,
                Book = book,
                PersonId = person.Id,
                Person = person
            };
            var mock = new Mock<IPersonService>();
            mock.Setup(service => service.Get(personId))
                .Returns(person);
            mock.Setup(service => service.GetBook(book.Id))
                .Returns(book);
            mock.Setup(service => service.GetPersonBook(personBook.PersonId, personBook.BookId))
                .Returns(personBook);
            var controller = new PeopleController(mock.Object);

            var result = controller.AddPersonBook(personBook);

            mock.Verify(service => service.AddBook(personBook), Times.Never);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<ConflictResult>(result.Result);
        }

        [Test]
        [TestCase(1, 5, "test")]
        public void AddAlreadyExistingBookToPerson(int personId, int bookId, string title)
        {
            var person = GetTestPeople().FirstOrDefault(p => p.Id == personId);
            var book = new Book() { Id = bookId, Title = title };
            var personBook = new PersonBook()
            {
                BookId = book.Id,
                Book = book,
                PersonId = person.Id,
                Person = person
            };
            var mock = new Mock<IPersonService>();
            mock.Setup(service => service.Get(personId))
                .Returns(person);
            mock.Setup(service => service.GetBook(bookId));
            var controller = new PeopleController(mock.Object);

            var result = controller.AddPersonBook(personBook);

            mock.Verify(service => service.AddBook(personBook), Times.Never);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        [TestCase("person1", "person1", null)]
        public void DeleteExistingPeople(string firstName, string lastName, string middleName)
        {
            var people = GetTestPeople()
                .Where
                (
                    p => 
                    p.FirstName == firstName && 
                    p.LastName == lastName && 
                    p.MiddleName == middleName
                );
            var mock = new Mock<IPersonService>();
            mock.Setup(service => service.GetAll(firstName, lastName, middleName))
                .Returns(people);
            mock.Setup(service => service.Delete(people.FirstOrDefault()));
            var controller = new PeopleController(mock.Object);

            var result = controller.Delete(firstName, lastName, middleName);

            mock.Verify(service => service.Delete(It.IsAny<Person>()), Times.Exactly(people.Count()));
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        [TestCase("person3", "person3", null)]
        public void DeleteNotExistingPeople(string firstName, string lastName, string middleName)
        {
            var people = GetTestPeople()
                .Where
                (
                    p =>
                    p.FirstName == firstName &&
                    p.LastName == lastName &&
                    p.MiddleName == middleName
                );
            var mock = new Mock<IPersonService>();
            mock.Setup(service => service.GetAll(firstName, lastName, middleName))
                .Returns(people);
            mock.Setup(service => service.Delete(people.FirstOrDefault()));
            var controller = new PeopleController(mock.Object);

            var result = controller.Delete(firstName, lastName, middleName);

            mock.Verify(service => service.Delete(It.IsAny<Person>()), Times.Never);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        [TestCase(1)]
        public void DeleteExistingPerson(int personId)
        {
            var person = GetTestPeople().FirstOrDefault(p => p.Id == personId);
            var mock = new Mock<IPersonService>();
            mock.Setup(service => service.Get(personId))
                .Returns(person);
            mock.Setup(service => service.Delete(person));
            var controller = new PeopleController(mock.Object);

            var result = controller.Delete(personId);

            mock.Verify(service => service.Delete(person));
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        [TestCase(4)]
        public void DeleteNotExistingPerson(int personId)
        {
            var person = GetTestPeople().FirstOrDefault(p => p.Id == personId);
            var mock = new Mock<IPersonService>();
            mock.Setup(service => service.Get(personId))
                .Returns(person);
            mock.Setup(service => service.Delete(person));
            var controller = new PeopleController(mock.Object);

            var result = controller.Delete(personId);

            mock.Verify(service => service.Delete(person), Times.Never);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        [TestCase(1, 2)]
        public void DeleteExistingBookFromPerson(int personId, int bookId)
        {
            var person = GetTestPeople().FirstOrDefault(p => p.Id == personId);
            var book = person.PeopleBooks.FirstOrDefault(pb => pb.BookId == bookId).Book;
            var personBook = new PersonBook()
            {
                BookId = book.Id,
                Book = book,
                PersonId = person.Id,
                Person = person
            };
            var mock = new Mock<IPersonService>();
            mock.Setup(service => service.GetPersonBook(personId, bookId))
                .Returns(personBook);
            var controller = new PeopleController(mock.Object);

            var result = controller.Delete(personBook);

            mock.Verify(service => service.DeletePersonBook(personBook));
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        [TestCase(1, 2, 3)]
        public void DeleteNotExistingBookFromPerson(int otherPersonId, int personId, int bookId)
        {
            var person = GetTestPeople().FirstOrDefault(p => p.Id == personId);
            var book = GetTestPeople()
                .FirstOrDefault(p => p.Id == otherPersonId)
                .PeopleBooks
                .FirstOrDefault(pb => pb.BookId == bookId)
                .Book;
            var personBook = new PersonBook()
            {
                BookId = book.Id,
                Book = book,
                PersonId = person.Id,
                Person = person
            };
            var mock = new Mock<IPersonService>();
            mock.Setup(service => service.GetPersonBook(personId, bookId));
            var controller = new PeopleController(mock.Object);

            var result = controller.Delete(personBook);

            mock.Verify(service => service.DeletePersonBook(personBook), Times.Never);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        [TestCase(1, "person3", "person3")]
        public void UpdateExistingPerson(int oldPersonId, string firstName, string lastName)
        {
            var person = GetTestPeople().FirstOrDefault(p => p.Id == oldPersonId);
            var mock = new Mock<IPersonService>();
            mock.Setup(service => service.Get(person.FirstName, person.LastName, person.MiddleName))
                .Returns(person);
            mock.Setup(service => service.UpdatePerson(person, firstName, lastName, null, null));
            var controller = new PeopleController(mock.Object);

            var result = controller.Post(person, firstName, lastName, null, null);

            mock.Verify(service => service.UpdatePerson(person, firstName, lastName, null, null));
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        [TestCase(3, "person3", "person3")]
        public void UpdateNotExistingPerson(
                int oldPersonId, 
                string firstName, 
                string lastName
            )
        {
            var person = new Person() { Id = oldPersonId, FirstName = "test", LastName = "test" };
            var mock = new Mock<IPersonService>();
            mock.Setup(service => service.Get(person.FirstName, person.LastName, person.MiddleName));
            mock.Setup(service => service.UpdatePerson(person, firstName, lastName, null, null));
            var controller = new PeopleController(mock.Object);

            var result = controller.Post(person, firstName, lastName, null, null);

            mock.Verify(service => service.UpdatePerson
                (
                    person, 
                    firstName, 
                    lastName, 
                    null, 
                    null
                ), 
                Times.Never);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        private List<Person> GetTestPeople()
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

            var people = new List<Person>()
            {
                new Person() { Id = 1, FirstName = "person1", LastName = "person1" },
                new Person() { Id = 2, FirstName = "person2", LastName = "person2" },
            };

            var person1Books = new List<PersonBook>()
            {
                new PersonBook() { Book = books[1], BookId = books[1].Id, Person = people[0], PersonId = people[0].Id },
                new PersonBook() { Book = books[2], BookId = books[2].Id, Person = people[0], PersonId = people[0].Id },
            };

            var person2Books = new List<PersonBook>()
            {
                new PersonBook() { Book = books[0], BookId = books[0].Id, Person = people[1], PersonId = people[1].Id },
            };

            var book1People = new List<PersonBook>()
            {
                new PersonBook() { Book = books[0], BookId = books[0].Id, Person = people[1], PersonId = people[1].Id },
            };

            var book2People = new List<PersonBook>()
            {
                new PersonBook() { Book = books[1], BookId = books[1].Id, Person = people[0], PersonId = people[0].Id },
            };

            var book3People = new List<PersonBook>()
            {
                new PersonBook() { Book = books[2], BookId = books[2].Id, Person = people[0], PersonId = people[0].Id },
            };

            people[0].PeopleBooks = person1Books;
            people[1].PeopleBooks = person2Books;

            books[0].PeopleBooks = book1People;
            books[1].PeopleBooks = book2People;
            books[2].PeopleBooks = book3People;

            return people;
        }
    }
}