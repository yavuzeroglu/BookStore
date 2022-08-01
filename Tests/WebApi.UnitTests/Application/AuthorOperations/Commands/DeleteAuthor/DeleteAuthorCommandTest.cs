using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using Webapi.Application.AuthorOperations.Commands;
using Webapi.DBOperations;
using Webapi.Entities;
using Xunit;

namespace Tests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTest : IClassFixture<CommonTestFixture>{
        private readonly BookStoreDbContext _context;
        
        public DeleteAuthorCommandTest(CommonTestFixture testFixture){
            _context = testFixture.Context;
        }
        
        [Fact]
        public void WhenGivenAuthorIdDoesNotExistInDb_InvalidOperationexception_ShouldBeReturn(){
            // Arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 0;
            // Act - Assert
            FluentActions
                .Invoking(() => command.Handle()).Should()
                .Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek Yazar Bulunamadı.");
        }

        [Fact]
        public void WhenGivenAuthorIdExistInDb_Author_ShouldBeDeleted(){
            // Arrange
            var authorTest = new Author(){
                Name="Cemal Süreya",
                Birthday = DateTime.Now.AddYears(-43)
            };
            _context.Authors.Add(authorTest);
            _context.SaveChanges();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = authorTest.Id;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var author = _context.Authors.SingleOrDefault(x => x.Id == authorTest.Id);
            author.Should().BeNull();
        }

    }
}