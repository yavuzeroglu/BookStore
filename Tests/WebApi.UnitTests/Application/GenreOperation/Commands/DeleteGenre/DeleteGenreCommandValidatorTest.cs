using FluentAssertions;
using TestSetup;
using Webapi.Application.GenreOperations.DeleteGenre;
using Xunit;

namespace Tests.Application.GenreOperation.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenGivenGenreIdIsNotGreaterThenZero_Validator_ShouldReturnError(){
            // Arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = 0;

            // Act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGivenGenreIdIsGreaterThanZero_Validator_ShouldNotReturnError(){
            // Arrange
            var command = new DeleteGenreCommand(null);
            command.GenreId = 1;
            // Act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var resutlt = validator.Validate(command);

            // Assert
            resutlt.Errors.Count.Should().Be(0);
        }
    }
}