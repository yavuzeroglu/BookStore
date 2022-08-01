using System;
using System.Linq;
using Webapi.DBOperations;

namespace Webapi.Application.AuthorOperations.Commands.UpdateAuthors
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model { get; set; }
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public UpdateAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author == null)
                throw new InvalidOperationException("Yazar Bulunamadı.");

            if (_dbContext.Authors.Any(x=> x.Name.ToLower() == Model.Name.ToLower() && x.Id != AuthorId))
                throw new InvalidOperationException("Aynı İsimli Bir Yazar Zaten Mevcut");
            
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