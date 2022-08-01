using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Webapi.DBOperations;

namespace Webapi.Application.GenreOperations.Queries.GetGenresDetail
{
    public class GetGenresDetailQuery
    {
        public int GenreId { get; set; }
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;
        public GetGenresDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x=>x.IsActive&& x.Id==GenreId);
            if (genre is null)
                throw new InvalidOperationException("Kitap Türü Bulunamadı.");
            
            return _mapper.Map<GenreDetailViewModel>(genre);
        }
    }

    public class GenreDetailViewModel{
        public int Id { get; set; }
        public string Name { get; set; }
    }
}