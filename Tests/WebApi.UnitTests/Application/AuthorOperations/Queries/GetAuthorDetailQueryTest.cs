using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using Webapi.Application.AuthorOperations.Queries.GetAuthorDetail;
using Webapi.DBOperations;
using Webapi.Entities;
using Xunit;

namespace Tests.Application.AuthorOperations.Queries
{
    public class GetAuthorDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenAuthorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturn(){
            
            // Arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mapper);
            query.AuthorId=0;

            // Act - Assert
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadı");
        }

        [Fact]
        public void WhenGivenAuthorIdDoesExistInDb_Author_ShouldBeReturn(){
            // Arrange
            var author = new Author{
                Name = "WhenGivenAuthor",Birthday=new DateTime(1990,01,01)};
            _context.Authors.Add(author);
            _context.SaveChanges();

            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mapper);
            query.AuthorId = author.Id;
            // Act 
            var authorTest = FluentActions.Invoking(() => query.Handle()).Invoke();
            
            // Assert
            authorTest.Should().NotBeNull();
            authorTest.Name.Should().Be(author.Name);
            authorTest.Birthday.Should().Be(author.Birthday);
            
        }
    }
}