using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Webapi.Common;
using Webapi.DBOperations;

namespace Webapi.BookOperations.GetBooks{
    public class GetBookCommand
    {
        private readonly BookStoreDbContext _dbContext;

        public GetBookCommand(BookStoreDbContext dbContext){
            _dbContext = dbContext;
        }

        public Book Handle(int id)
        {
            
            var book = _dbContext.Books.Where(book=>book.Id == id).SingleOrDefault();
            // BooksViewModel booksViewModel = new BooksViewModel();
            
            return book;

        }
        public class BooksViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }
        }
    }
}