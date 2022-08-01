using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Webapi.DBOperations;
using Webapi.Entities;

namespace Webapi.Application.AuthorOperations.Queries.GetAuthor
{
    public class GetAuthorQuery
    {
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;

        public GetAuthorQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public List<AuthorViewModel> Handle()
        {
            var authorList = _context.Authors.OrderBy(x=> x.Id).ToList<Author>();
            List<AuthorViewModel> vm = _mapper.Map<List<AuthorViewModel>>(authorList);

            return vm;
        }
    }

    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
    }
}