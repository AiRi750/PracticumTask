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
    public class AuthorsControllerTests
    {
        [Test]
        public void GetAllAuthors()
        {
            var mock = new Mock<IAuthorService>();
            mock.Setup(service => service.GetAll()).Returns(GetTestAuthors());
            var controller = new AuthorsController(mock.Object);

            var result = controller.GetAll();

            mock.Verify(service => service.GetAll());
            Assert.AreEqual(GetTestAuthors().Count, result.Count());
        }

        [Test]
        [TestCase("first")]
        public void GetExistingAuthors(string firstName)
        {
            var mock = new Mock<IAuthorService>();
            mock.Setup(service => service.Get(firstName))
                .Returns(GetTestAuthors().Where(x => x.FirstName == firstName));
            var controller = new AuthorsController(mock.Object);

            var authors = controller.Get(firstName);

            mock.Verify(service => service.Get(firstName));
            Assert.IsInstanceOf<Task<IActionResult>>(authors);
            Assert.IsInstanceOf<OkObjectResult>(authors.Result);
        }

        [Test]
        [TestCase("fifth")]
        public void GetNotExistingAuthors(string firstName)
        {
            var mock = new Mock<IAuthorService>();
            mock.Setup(service => service.Get(firstName))
                .Returns(GetTestAuthors().Where(x => x.FirstName == firstName));
            var controller = new AuthorsController(mock.Object);

            var authors = controller.Get(firstName);

            mock.Verify(service => service.Get(firstName));
            Assert.IsInstanceOf<Task<IActionResult>>(authors);
            Assert.IsInstanceOf<NotFoundResult>(authors.Result);
        }

        [Test]
        [TestCase(5, "test", "test", null)]
        public void AddNotExistingAuthor(int id, string firstName, string lastName, string middleName)
        {
            var author = new Author() { Id = id, FirstName = lastName, LastName = lastName };
            var mock = new Mock<IAuthorService>();
            mock.Setup(service => service.Add(author));
            mock.Setup(service => service.Get(firstName, lastName, null))
                .Returns(GetTestAuthors().FirstOrDefault
                (
                    x => 
                    x.FirstName == firstName &&
                    x.LastName == lastName &&
                    x.MiddleName == middleName
                ));
            var controller = new AuthorsController(mock.Object);

            var result = controller.Add(author);

            mock.Verify(service => service.Add(author));
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        [TestCase(3, "third", "first", null)]
        public void AddExistingAuthor(int id, string firstName, string lastName, string middleName)
        {
            var author = new Author() { Id = id, FirstName = firstName, LastName = lastName };
            var mock = new Mock<IAuthorService>();
            mock.Setup(service => service.Add(author));
            mock.Setup(service => service.Get(firstName, lastName, null))
                .Returns(GetTestAuthors().FirstOrDefault
                (
                    x => 
                    x.FirstName == firstName &&
                    x.LastName == lastName &&
                    x.MiddleName == middleName
                ));
            var controller = new AuthorsController(mock.Object);

            var result = controller.Add(author);

            mock.Verify(service => service.Add(author), Times.Never);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<ConflictResult>(result.Result);
        }

        [Test]
        [TestCase(4, "first", "fourth", null)]
        public void DeleteExistingAuthor(int id, string firstName, string lastName, string middleName)
        {
            var mock = new Mock<IAuthorService>();
            var author = new Author() 
                { 
                    Id = id, 
                    FirstName = firstName, 
                    LastName = lastName, 
                    MiddleName = middleName 
                };
            mock.Setup(service => service.Delete(author));
            mock.Setup(service => service.Get(firstName, lastName, middleName))
                .Returns(GetTestAuthors().FirstOrDefault
                (
                    x => 
                    x.FirstName == firstName &&
                    x.LastName == lastName &&
                    x.MiddleName == middleName
                ));
            var controller = new AuthorsController(mock.Object);

            var result = controller.Delete(firstName, lastName, middleName);

            mock.Verify(service => service.Delete(It.IsAny<Author>()));
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        [TestCase(5, "test", "test", null)]
        public void DeleteNotExistingAuthor(int id, string firstName, string lastName, string middleName)
        {
            var mock = new Mock<IAuthorService>();
            var author = new Author()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName
            };
            mock.Setup(service => service.Delete(author));
            mock.Setup(service => service.Get(firstName, lastName, middleName))
                .Returns(GetTestAuthors().FirstOrDefault
                (
                    x =>
                    x.FirstName == firstName &&
                    x.LastName == lastName &&
                    x.MiddleName == middleName
                ));
            var controller = new AuthorsController(mock.Object);

            var result = controller.Delete(firstName, lastName, middleName);

            mock.Verify(service => service.Delete(It.IsAny<Author>()), Times.Never);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        private List<Author> GetTestAuthors()
            => new List<Author>()
            {
                new Author(){ Id = 1, FirstName = "first", LastName = "first", MiddleName = "first" },
                new Author(){ Id = 2, FirstName = "second", LastName = "second", MiddleName = "second" },
                new Author(){ Id = 3, FirstName = "third", LastName = "first" },
                new Author(){ Id = 4, FirstName = "first", LastName = "fourth" },
            };
    }
}