using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using Webapi.Application.AuthorOperations.Commands.UpdateAuthors;
using Webapi.DBOperations;
using Webapi.Entities;
using Xunit;

namespace Tests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTest : IClassFixture<CommonTestFixture>{
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandTest(CommonTestFixture testFixture){
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenAuthorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn(){
            // Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = -1;
            // Act - Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadı.");
        }

        [Fact]
        public void WhenGivenNameIsSameWithAnotherAuthor_InvalidOperationException_ShouldBeReturn(){
            var authorDb = new Author{
                Name = "Name Surname", Birthday = new DateTime(2000,02,02)
            };
            var author2Db = new Author{
                Name = "2Name Surname"
            };
            _context.Authors.Add(authorDb);
            _context.Authors.Add(author2Db);
            _context.SaveChanges();

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = authorDb.Id;
            command.Model = new UpdateAuthorModel(){
                Name = author2Db.Name, Birthday = new DateTime(2000,02,02)};

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı İsimli Bir Yazar Zaten Mevcut");
        }
        [Fact]
        public void WhenGivenAuthorIdExistsInDb_Author_ShouldBeUpdated(){
            // Arrange
            var authorDb = new Author() {
                Name = "Sunay Akın", 
                Birthday = new DateTime(1962,09,12)};
            var authorCompared = new Author() {
                Name = authorDb.Name,
                Birthday = authorDb.Birthday };
            _context.Authors.Add(authorDb);
            _context.SaveChanges();

            var command = new UpdateAuthorCommand(_context);
            command.AuthorId = authorDb.Id;
            command.Model = new UpdateAuthorModel{
                Name = "UpdatedName",
                Birthday = new DateTime(1962,10,12)
            };

            // Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var authorUpdate = _context.Authors.SingleOrDefault(x => x.Id == authorDb.Id);
            authorUpdate.Should().NotBeNull();
            authorUpdate.Name.Should().NotBe(authorCompared.Name);
            authorUpdate.Birthday.Should().NotBe(authorCompared.Birthday);
            


            

        }
    }
}