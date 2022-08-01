using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using Webapi.Application.BookOperations.UpdateBook;
using Webapi.DBOperations;
using Webapi.Entities;
using Xunit;
using static Webapi.Application.BookOperations.UpdateBook.UpdateBookCommand;

namespace Tests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenBookIdDoesNotExistDb_InvalidOperationException_ShouldBeReturn(){
            //Assert
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = -1;

            //Act - Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().
                And.Message.Should().Be("Güncellenecek Kitap Bulunamadı!");
        }

        [Fact]
        public void WhenGivenBookIdExistsInDb_Book_ShouldBeUpdated(){
            // Arrange
            var BookInDb = 
            new Book{
                Title="WhenGivenBookIdExistsInDb_Book_ShouldBeUpdated", PageCount = 100,GenreId = 1, AuthorId=1,IsPublished=true, PublishDate= new DateTime(1996,06,19)
            };
            var CompareBook = new Book{
                Title = BookInDb.Title,
                PageCount = BookInDb.PageCount,
                GenreId = BookInDb.GenreId,
                AuthorId = BookInDb.AuthorId,
                IsPublished = BookInDb.IsPublished,
                PublishDate = BookInDb.PublishDate

            };
            _context.Books.Add(BookInDb);
            _context.SaveChanges();

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = BookInDb.Id;
            command.Model = new UpdateBookModel{
                Title = "UpdatedBook", Author = 1,GenreId=1
            };

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var updateBook = _context.Books.SingleOrDefault(x => x.Id == BookInDb.Id);
            updateBook.Should().NotBeNull();
            updateBook.PageCount.Should().Be(CompareBook.PageCount);
            updateBook.PublishDate.Should().Be(CompareBook.PublishDate);
            updateBook.AuthorId.Should().Be(CompareBook.AuthorId);
            


        }
    }
}