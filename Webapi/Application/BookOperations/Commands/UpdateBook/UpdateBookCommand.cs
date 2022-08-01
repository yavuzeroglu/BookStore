using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Webapi.Common;
using Webapi.DBOperations;

namespace Webapi.Application.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public int BookId { get; set; }

        public UpdateBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x=> x.Id== BookId);
            if(book == null)
                throw new InvalidOperationException("Güncellenecek Kitap Bulunamadı!");
            
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;

            book.Title = Model.Title != default ? Model.Title : book.Title;

            
            _dbContext.SaveChanges();
        }

        public class UpdateBookModel
        {
            
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int Author { get; set; }

            public int PageCount { get; set; }
            

        }
    }
}