using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Webapi.Common;
using Webapi.DBOperations;

namespace Webapi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x=> x.Id== Model.Id);
            if(book == null)
                throw new InvalidOperationException("Boş Bırakılamaz");
            
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;

            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;

            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;

            book.Title = Model.Title != default ? Model.Title : book.Title;

            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();
        }

        public class UpdateBookModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }

        }
    }
}