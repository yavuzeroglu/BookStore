using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using Webapi.Application.AuthorOperations.Commands.CreateAuthors;
using Webapi.DBOperations;
using Webapi.Entities;
using Xunit;

namespace Tests.Application.AuthorOperations.Commands.CreateAuthor{
    public class CreateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTest(CommonTestFixture testFixture){
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorTitleIsGiven_InvalidOperationException_ShouldBeReturn(){
            // Arrange
            var author = new Author()
                { Name = "Test Test", Birthday = new DateTime(1996,02,19)};
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            command.Model = new CreateAuthorModel
            { Name = author.Name };

            // Act - Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Zaten Mevcut!");

        }

        [Fact]
        public void WhenValidInputAreGive_Author_ShouldBeCreated(){
            // Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            CreateAuthorModel model = new CreateAuthorModel()
                { Name = "Test Test", BirthDay = DateTime.Now.Date.AddYears(-20) };
            command.Model = model;
            
            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var author = _context.Authors.SingleOrDefault(x => x.Name == model.Name);
            author.Should().NotBeNull();
            author.Birthday.Should().Be(model.BirthDay);

        }
    }
}