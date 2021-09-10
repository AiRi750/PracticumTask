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
    class GenresControllerTests
    {
        [Test]
        public void GetAllGenres()
        {
            var mock = new Mock<IGenreService>();
            mock.Setup(service => service.GetAll()).Returns(GetTestGenres());
            var controller = new GenresController(mock.Object);

            var result = controller.Get();

            mock.Verify(service => service.GetAll());
            Assert.AreEqual(GetTestGenres().Count, result.Count());
        }

        [Test]
        [TestCase("second")]
        public void GetExistingGenre(string name)
        {
            var mock = new Mock<IGenreService>();
            mock.Setup(service => service.Get(name))
                .Returns(GetTestGenres().FirstOrDefault(x => x.Name == name));
            var controller = new GenresController(mock.Object);

            var result = controller.Get(name);

            mock.Verify(service => service.Get(name));
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        [TestCase("fifth")]
        public void GetNotExistingGenre(string name)
        {
            var mock = new Mock<IGenreService>();
            mock.Setup(service => service.Get(name))
                .Returns(GetTestGenres().FirstOrDefault(x => x.Name == name));
            var controller = new GenresController(mock.Object);

            var result = controller.Get(name);

            mock.Verify(service => service.Get(name));
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        [TestCase(2, "second")]
        public void AddExistingGenre(int id, string name)
        {
            var genre = new Genre() { Id = id, Name = name };
            var mock = new Mock<IGenreService>();
            mock.Setup(service => service.Get(name))
                .Returns(GetTestGenres().FirstOrDefault(x => x.Name == name));
            mock.Setup(service => service.Add(genre));
            var controller = new GenresController(mock.Object);

            var result = controller.Post(genre);

            mock.Verify(service => service.Add(genre), Times.Never);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<ConflictResult>(result.Result);
        }

        [Test]
        [TestCase(5, "fifth")]
        public void AddNotExistingGenre(int id, string name)
        {
            var genre = new Genre() { Id = id, Name = name };
            var mock = new Mock<IGenreService>();
            mock.Setup(service => service.Get(name))
                .Returns(GetTestGenres().FirstOrDefault(x => x.Name == name));
            mock.Setup(service => service.Add(genre));
            var controller = new GenresController(mock.Object);

            var result = controller.Post(genre);

            mock.Verify(service => service.Add(genre));
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        [TestCase(2, "second")]
        public void DeleteExistingGenre(int id, string name)
        {
            var genre = new Genre() { Id = id, Name = name };
            var mock = new Mock<IGenreService>();
            mock.Setup(service => service.Get(name))
                .Returns(GetTestGenres().FirstOrDefault(x => x.Name == name));
            mock.Setup(service => service.Delete(genre));
            var controller = new GenresController(mock.Object);

            var result = controller.Delete(name);

            mock.Verify(service => service.Delete(It.IsAny<Genre>()));
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        [TestCase(5, "fifth")]
        public void DeleteNotExistingGenre(int id, string name)
        {
            var genre = new Genre() { Id = id, Name = name };
            var mock = new Mock<IGenreService>();
            mock.Setup(service => service.Get(name))
                .Returns(GetTestGenres().FirstOrDefault(x => x.Name == name));
            mock.Setup(service => service.Delete(genre));
            var controller = new GenresController(mock.Object);

            var result = controller.Delete(name);

            mock.Verify(service => service.Delete(It.IsAny<Genre>()), Times.Never);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        private List<Genre> GetTestGenres()
            => new List<Genre>()
            {
                new Genre(){ Id = 1, Name = "first" },
                new Genre(){ Id = 2, Name = "second" },
                new Genre(){ Id = 3, Name = "third" },
                new Genre(){ Id = 4, Name = "fourth" },
            };
    }
}
