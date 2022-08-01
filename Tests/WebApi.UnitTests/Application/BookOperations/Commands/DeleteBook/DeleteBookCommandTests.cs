using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using Webapi.Application.BookOperations.DeleteBook;
using Webapi.DBOperations;
using Webapi.Entities;
using Xunit;

namespace Tests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = -1;
            // act - assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek Kitap Bulunamadı");


        }
        [Fact]
        public void WhenAlreadyExistBookIdIsGiven_Book_ShouldBeDeleted()
        {
            // Arrange
            var deleteBook = new Book() 
                { Title = "Delete Book", PageCount = 100,IsPublished = true, PublishDate = new DateTime(2002, 02, 02),AuthorId = 1, GenreId = 1, };
            _context.Books.Add(deleteBook);
            _context.SaveChanges();

            var command = new DeleteBookCommand(_context);
            command.BookId = deleteBook.Id;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            Book book1 = _context.Books.SingleOrDefault(x => x.Id == deleteBook.Id);
            var book = book1;
            book.Should().BeNull();
        }

    }
}