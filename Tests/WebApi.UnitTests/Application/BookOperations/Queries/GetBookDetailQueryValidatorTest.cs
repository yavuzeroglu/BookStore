using FluentAssertions;
using TestSetup;
using Webapi.Application.BookOperations.GetBookDetail;
using Xunit;

namespace Tests.Application.BookOperations.Queries
{
    public class GetBookDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenGivenBookIdIsNotGreaterThanZero_Validator_ShouldReturnError(){
            // Arrange
            GetBookDetailQuery query = new GetBookDetailQuery(null,null);
            query.BookId = 0;
            // Act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGivenBookIdIsGreaterThanZero_Validator_ShouldNotReturnError(){
            // Arrange
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = 1;
            
            // Act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().Be(0);

        }
    }
}