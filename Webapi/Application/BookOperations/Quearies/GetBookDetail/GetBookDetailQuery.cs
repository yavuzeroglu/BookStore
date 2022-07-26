using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webapi.Common;
using Webapi.DBOperations;

namespace Webapi.Application.BookOperations.GetBookDetail{

    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;

        private readonly IMapper _mapper;
        public int BookId { get; set; }

        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            
            var book = _dbContext.Books.Include(x=> x.Author).Include(x => x.Genre).Where(book=>book.Id == BookId).SingleOrDefault();
            if(book is null)
                throw new InvalidOperationException("Kitap Bulunamadı");

            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book); 
             
            
            return vm;

        }
        public class BookDetailViewModel
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public string Author { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            
        }
    }
}