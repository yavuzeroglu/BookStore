using FluentAssertions;
using TestSetup;
using Webapi.Application.GenreOperations.Commands.CreateGenre;
using Webapi.Application.GenreOperations.CreateGenre;
using Webapi.DBOperations;
using Xunit;

namespace Tests.Application.GenreOperation.Commands.CreateGenre
{

    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("N")]
        [InlineData("Nam")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError(string name)
        {
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreModel() { Name = name};

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError(){
            // Arrange 
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreModel() { Name = "Name" };
            //Act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);
            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}