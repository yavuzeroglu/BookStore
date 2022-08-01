using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using Webapi.Application.BookOperations.GetBookDetail;
using Webapi.DBOperations;
using Webapi.Entities;
using Xunit;
using static Webapi.Application.BookOperations.GetBookDetail.GetBookDetailQuery;

namespace Tests.Application.BookOperations.Queries
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenBookIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId = 999;

          

            // Act - Assert
            

            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should()
                .Be("Kitap Bulunamadı"); 
        }

        [Fact]
        public void WhenValidInputBookIdGiven_Book_GetDetail(){
            //Arrange
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            BookDetailViewModel model = new BookDetailViewModel();
            query.BookId = 1;
            
            //Act
            FluentActions.Invoking(() => query.Handle()).Invoke();

            //Assert
            var book = _context.Books.SingleOrDefault(x => x.Id == 1);
            book.Should().NotBeNull();
        }
    }
}