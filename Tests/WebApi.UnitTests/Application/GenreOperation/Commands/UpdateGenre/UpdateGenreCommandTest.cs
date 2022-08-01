using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using Webapi.Application.GenreOperations.UpdateGenre;
using Webapi.DBOperations;
using Webapi.Entities;
using Xunit;

namespace Tests.Application.GenreOperation.Commands.UpdateGenre
{
    public class UpdateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenGenreIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
        {

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = -1;
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı.");
        }
        [Fact]
        public void WhenGivenNameIsSameNameWithAnohterGenre_InvalidOperationException_ShoulBeReturn()
        {
            //Arrange
            var genreTest = new Genre() { Name = "SameName1" };
            var genreTest1 = new Genre() { Name = "SameName2" };

            _context.Genres.Add(genreTest);
            _context.Genres.Add(genreTest1);
            _context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = genreTest.Id;
            command.Model = new UpdateGenreModel()
            { Name = genreTest1.Name };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı İsimli Bir Kitap Zaten Mevcut");
        }

        [Fact]
        public void WhenGivenGenreIdExistsInDb_Genre_ShouldBeUpdated()
        {
            // Arrange 
            var genreInDb = new Genre() { Name = "TestGenre" };
            var genreCompared = new Genre() { Name = genreInDb.Name };
            _context.Genres.Add(genreInDb);
            _context.SaveChanges();

            var command = new UpdateGenreCommand(_context);
            command.GenreId = genreInDb.Id;
            command.Model = new UpdateGenreModel{
                Name = "UpdateName",
                IsActive = false
            };

            // Act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();
            
            // Assert
            var genreUpdate = _context.Genres.SingleOrDefault(x => x.Id == genreInDb.Id);
            genreUpdate.Should().NotBeNull();
            genreUpdate.Name.Should().NotBe(genreCompared.Name);
            genreUpdate.IsActive.Should().NotBe(genreCompared.IsActive);
        }
    }
}