using FluentAssertions;
using TestSetup;
using Webapi.Application.BookOperations.UpdateBook;
using Xunit;
using static Webapi.Application.BookOperations.UpdateBook.UpdateBookCommand;

namespace Tests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"Lord",1,1)]
        [InlineData(1,"",1,1)]
        [InlineData(0,"Lor",1,1)]
        [InlineData(0,"Lord",0,1)]
        [InlineData(1,"Lord",1,0)]
        [InlineData(0," ",0,1)]
        [InlineData(0,"Lord",0,0)]
        [InlineData(1," ",0,0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId, string title,int pageCount, int genreId)
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                Title = title, 
                GenreId=genreId,
                PageCount = pageCount 
            };
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldNotBeReturnError(){
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 1;
            command.Model = new UpdateBookModel(){
                Title = "Update Book",
                Author = 1,
                GenreId = 1,
                PageCount = 100
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
            
        }
    }
}