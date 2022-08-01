using System;
using FluentAssertions;
using TestSetup;
using Webapi.Application.AuthorOperations.Commands.CreateAuthors;
using Xunit;

namespace Tests.Application.AuthorOperations.Commands.CreateAuthor{
    public class CreateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("N")]
        [InlineData("Na")]
        [InlineData("Nam")]
        [InlineData("Name")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name){
            // Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            command.Model = new CreateAuthorModel(){
                Name = name, BirthDay = DateTime.Now.AddYears(-20)
            };
            
            // Act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenAuthorIsUnder8_Validator_ShouldReturnError(){
            // Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            command.Model = new CreateAuthorModel{
                Name="WhenAuthorIsUnder8",
                BirthDay = DateTime.Now.AddYears(-8).AddDays(1)
            };
            // Act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShoudNotBeReturnError(){
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            command.Model = new CreateAuthorModel(){
                Name="Can Yücel",
                BirthDay = DateTime.Now.AddYears(-40)
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}