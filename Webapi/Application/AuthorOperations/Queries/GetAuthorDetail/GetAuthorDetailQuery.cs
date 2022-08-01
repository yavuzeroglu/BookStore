using System;
using System.Linq;
using AutoMapper;
using Webapi.DBOperations;

namespace Webapi.Application.AuthorOperations.Queries.GetAuthorDetail{
    public class GetAuthorDetailQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }

        public GetAuthorDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle(){
            var author = _dbContext.Authors.Where(author => author.Id == AuthorId).SingleOrDefault();
            if(author is null)
                throw new InvalidOperationException("Yazar Bulunamadı");

            AuthorDetailViewModel authorDetailModel = _mapper.Map<AuthorDetailViewModel>(author);

            return authorDetailModel;
        }
    }

    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}