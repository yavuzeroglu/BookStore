using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using Webapi.Application.GenreOperations.Queries.GetGenresDetail;
using Webapi.DBOperations;
using Xunit;

namespace Tests.Application.GenreOperation.Queries
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture){
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenGivenGenreIdDoesExistInDb_InvalidOperationException_ShouldBeReturn(){
            // Arrange
            GetGenresDetailQuery query = new GetGenresDetailQuery(_context,_mapper);
            query.GenreId = 99;

            //Act - Assert
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı.");
        }
    }
}