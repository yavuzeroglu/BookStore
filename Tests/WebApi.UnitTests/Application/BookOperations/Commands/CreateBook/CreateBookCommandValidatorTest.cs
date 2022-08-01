using System;
using FluentAssertions;
using TestSetup;
using Webapi.Application.BookOperations.CreateBook;
using Xunit;
using static Webapi.Application.BookOperations.CreateBook.CreateBookCommand;

namespace Tests.Application.BookOperations.Commands.CreateCommands
{
    public class CreateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord Of The Rings", 0, 0, 0)]
        [InlineData("Lord Of The Rings", 0, 1, 0)]
        [InlineData("Lord Of The Rings", 0, 0, 1)]
        [InlineData("", 0, 0, 1)]
        [InlineData("Lord Of The Rings", 100, 0, 0)]
        [InlineData("Lor", 100, 1, 1)]
        [InlineData("Lor", 100, 1, 1)]
        [InlineData("Lord ", 100, 1, 0)]
        [InlineData(" ", 100, 1, 1)]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.AddYears(-1),
                GenreId = genreId,
                AuthorId = authorId
            };
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            //act

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShoulBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Title",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1,
                AuthorId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1, 
                AuthorId =1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}