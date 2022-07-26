using System;
using System.Linq;
using Webapi.DBOperations;

namespace Webapi.Application.AuthorOperations.Commands.UpdateAuthors
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model { get; set; }
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public UpdateAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author == null)
                throw new InvalidOperationException("Yazar Bulunamadı.");
            
            author.Name = Model.Name != default ? Model.Name : author.Name;
            author.Birthday = Model.Birthday != default ? Model.Birthday : author.Birthday;

            _dbContext.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }

        public DateTime Birthday { get; set; }

    }
}