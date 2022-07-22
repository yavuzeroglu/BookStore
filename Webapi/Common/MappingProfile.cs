using AutoMapper;
using static Webapi.BookOperations.CreateBook.CreateBookCommand;
using static Webapi.BookOperations.GetBookDetail.GetBookDetailQuery;

namespace Webapi.Common{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book,BookDetailViewModel>().ForMember(dest => dest.Genre, opt=>opt.MapFrom(src=> ((GenreEnum) src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt=>opt.MapFrom(src=> ((GenreEnum) src.GenreId).ToString()));

        }
    }
}