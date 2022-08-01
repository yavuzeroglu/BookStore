using System;
using FluentAssertions;
using TestSetup;
using Webapi.Application.AuthorOperations.Commands.UpdateAuthors;
using Xunit;

namespace Tests.Application.AuthorOperations.Commands.UpdateAuthor{
    public class UpdateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("N")]
        [InlineData("Na")]
        [InlineData("Nam")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name){
            // Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel{ Name = name };

            // Act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public void WhenAuthorIsUnder8_Validator_ShouldReturnError(){
            // Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel{
                Name = "Test Name", Birthday= DateTime.Now.AddYears(-8).AddDays(1)};
            
            // Act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            
            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError(){
            // Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel{ 
                Name="Küçük İskender", 
                Birthday = new DateTime(1964,05,28) 
                };
            
            // Act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);

        }
    }
}