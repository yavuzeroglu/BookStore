using System;
using Webapi.DBOperations;
using Webapi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
            new Book{Title = "Lean Startup",GenreId = 1, PageCount = 200, PublishDate = new DateTime(2001, 06, 12),AuthorId = 2,IsPublished = true},

            new Book{Title = "Herland", GenreId = 2, PageCount = 250, PublishDate = new DateTime(2010, 05, 23),IsPublished = true,AuthorId = 3},

            new Book{Title = "Dune", GenreId = 2, PageCount = 550, PublishDate = new DateTime(2001, 12, 21),IsPublished = true,AuthorId = 1});
        }
        // public static void DeleteBook(this BookStoreDbContext context){

        // }
    }
}