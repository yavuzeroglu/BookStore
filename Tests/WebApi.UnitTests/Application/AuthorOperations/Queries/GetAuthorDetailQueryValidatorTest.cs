using FluentAssertions;
using TestSetup;
using Webapi.Application.AuthorOperations.Queries.GetAuthorDetail;
using Xunit;

namespace Tests.Application.AuthorOperations.Queries
{
    public class GetAuthorDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenGivenAuthorIdIsNotZeroGreaterThanZero_Validator_ShouldBeReturnError(){
            // Arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null,null);
            query.AuthorId = 0;

            // Act
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGivenAuthorIdIsGreaterThanZero_Validator_ShouldNotBeReturnError(){
            // arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null,null);
            query.AuthorId = 1;

            // act
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}