using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using Webapi.Application.GenreOperations.CreateGenre;
using Webapi.DBOperations;
using Webapi.Entities;
using Xunit;

namespace Tests.Application.GenreOperation.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            var genre = new Genre()
            {
                Name = "Test",
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel() { Name = genre.Name };

            // act - assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Zaten Mevcut!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            // Arrange
            CreateGenreCommand command = new CreateGenreCommand(_context);
            CreateGenreModel model = new CreateGenreModel()
            {
                Name = "WhenValidInputsAreGiven_Genre_ShouldBeCreated"
            };
            command.Model = model;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var genre = _context.Genres.SingleOrDefault(x => x.Name == model.Name);

            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
            genre.IsActive.Should().Be(true);

        }
    }
}