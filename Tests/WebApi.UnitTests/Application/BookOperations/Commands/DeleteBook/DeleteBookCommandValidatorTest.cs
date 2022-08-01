using FluentAssertions;
using TestSetup;
using Webapi.Application.BookOperations.DeleteBook;
using Xunit;

namespace Tests.Application.BookOperations.Commands.DeleteBook{
    public class DeleteBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenGivenBookIdIsNotGreaterThenZero_Validator_ShouldReturnError(){
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 0;
            //act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);  
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenGivenBookIdIsGreaterThenZero_Validator_ShouldNotReturnError(){
            // arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 1;

            // act
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}