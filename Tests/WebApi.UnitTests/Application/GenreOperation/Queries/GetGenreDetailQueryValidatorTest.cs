using FluentAssertions;
using TestSetup;
using Webapi.Application.GenreOperations.Queries.GetGenresDetail;
using Xunit;

namespace Tests.Application.GenreOperation.Queries
{
    public class GetGenreDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenGivenGenreIdIsNotGreaterThanZero_Validator_ShouldReturnError()
        {
            // Arrange
            GetGenresDetailQuery query = new GetGenresDetailQuery(null, null);
            query.GenreId = 0;

            // Act
            GetGenresDetailQueryValidator validator = new GetGenresDetailQueryValidator();
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public void WhenGivenGenreIdIsGreaterThanZero_Validator_ShouldNotReturnError(){
            GetGenresDetailQuery query = new GetGenresDetailQuery(null,null);
            query.GenreId = 1;

            GetGenresDetailQueryValidator validator = new GetGenresDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }

    }
}