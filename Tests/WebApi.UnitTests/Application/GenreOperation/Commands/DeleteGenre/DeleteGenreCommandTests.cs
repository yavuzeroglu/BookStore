using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using Webapi.Application.GenreOperations.DeleteGenre;
using Webapi.DBOperations;
using Webapi.Entities;
using Xunit;

namespace Tests.Application.GenreOperation.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistGenreIdGiven_InvalidOperationException_ShouldBeReturn(){
            // Arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = -1;
            // Act - Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı");
            
        }
        
        [Fact]
        public void WhenGivenGenreIdExistsInDb_Genre_ShouldBeDeleted(){
            // Arrange
            var newGenre = new Genre() { Name = "Delete Genre", IsActive = true};
            _context.Genres.Add(newGenre);
            _context.SaveChanges();

            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = newGenre.Id;
            
            // Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();
            
            // Assert
            var genre = _context.Genres.SingleOrDefault(x => x.Id == newGenre.Id);
            genre.Should().BeNull();
        }
    }
}