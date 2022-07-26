using System;
using System.Linq;
using AutoMapper;
using Webapi.DBOperations;
using Webapi.Entities;

namespace Webapi.Application.AuthorOperations.Commands.CreateAuthors
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model {get; set;}

        private readonly  BookStoreDbContext _context;
        
        private readonly IMapper _mapper;
        public CreateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Name == Model.Name);
            if(author is not null)
                throw new InvalidOperationException("Yazar Zaten Mevcut!");
            author = _mapper.Map<Author>(Model);

            _context.Authors.Add(author);
            _context.SaveChanges();

            
        }
    }

    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        
    }
} 