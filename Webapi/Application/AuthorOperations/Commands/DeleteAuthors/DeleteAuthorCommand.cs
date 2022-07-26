using System;
using System.Linq;
using Webapi.DBOperations;

namespace Webapi.Application.AuthorOperations.Commands
{
    public class DeleteAuthorCommand
    {
        private readonly BookStoreDbContext _dbContext;

        public int AuthorId { get; set; }
        public DeleteAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(author => author.Id == AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Silinecek Yazar Bulunamadı.");
            }
            else if (_dbContext.Books.Where(
                book => book.IsPublished == true && book.AuthorId == author.Id).Count()! > 0)
                {
                    throw new InvalidOperationException("Yazarın yayında bir kitap bulunduğundan dolayı kitap silinmedi!");
                }

            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }


    }
}