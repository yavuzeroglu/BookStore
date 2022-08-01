using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Webapi.Entities;

namespace Webapi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personel Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }
                );

                context.Books.AddRange(
                    new Book
                    {
                        //Id = 1, 
                        Title = "Lean Startup",
                        GenreId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 06, 12),
                        AuthorId = 2,
                        IsPublished = true
                    },

                    new Book
                    {
                        // Id = 2, 
                        Title = "Herland",
                        GenreId = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 05, 23),
                        IsPublished = true,
                        AuthorId = 3

                    },

                    new Book
                    {
                        // Id = 3, 
                        Title = "Dune",
                        GenreId = 2,
                        PageCount = 550,
                        PublishDate = new DateTime(2001, 12, 21),
                        IsPublished = true,
                        AuthorId = 1
                    }
                );
                if (context.Authors.Any())
                {
                    return;
                }
                context.Authors.AddRange(
                    new Author
                    {
                        Name = "Frank Herbert",
                        Birthday = new DateTime(1920, 10, 08)
                    },
                    new Author
                    {
                        Name = "Eric Ries",
                        Birthday = new DateTime(1978, 09, 22)
                    },
                    new Author
                    {
                        Name = "Charlotte Perkins",
                        Birthday = new DateTime(1890, 07, 03)
                    }
                );
                context.SaveChanges();

            }

        }
    }
}