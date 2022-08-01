using FluentAssertions;
using TestSetup;
using Webapi.Application.AuthorOperations.Commands;
using Xunit;

namespace Tests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenGivenAuthorIdIsNotGreaterThanZero_Validator_ShouldReturnError(){
            // Arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = 0;
            // Act
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var resut = validator.Validate(command); 
            // Assert
            resut.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public void WhenGivenAuthorIdIsGreaterThanZero_Validator_ShouldNotReturnError(){
            // Arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.AuthorId = 1;

            // Act
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}