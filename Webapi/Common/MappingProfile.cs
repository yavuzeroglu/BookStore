using AutoMapper;
using Webapi.Application.AuthorOperations.Commands;
using Webapi.Application.AuthorOperations.Commands.CreateAuthors;
using Webapi.Application.AuthorOperations.Queries.GetAuthor;
using Webapi.Application.AuthorOperations.Queries.GetAuthorDetail;
using Webapi.Application.GenreOperations.Queries.GetGenres;
using Webapi.Application.GenreOperations.Queries.GetGenresDetail;
using Webapi.Entities;
using static Webapi.Application.BookOperations.CreateBook.CreateBookCommand;
using static Webapi.Application.BookOperations.GetBookDetail.GetBookDetailQuery;


namespace Webapi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Book Maps
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().
                ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).
                ForMember(dest => dest.Author, opt => opt.MapFrom(src=> src.Author.Name)).
                ForMember(dest => dest.PublishDate, opt=>opt.MapFrom(src => src.PublishDate.ToShortDateString()));
            
            CreateMap<Book, BooksViewModel>().
                ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).
                ForMember(dest => dest.Author, opt => opt.MapFrom(src=> src.Author.Name)).
                ForMember(dest => dest.PublishDate, opt=>opt.MapFrom(src => src.PublishDate.ToShortDateString()));
            
            // Genre Maps
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            
            // Author Maps
            CreateMap<Author, AuthorViewModel>();
            
            CreateMap<Author, AuthorDetailViewModel>().ForMember(d=>d.Birthday,opt => opt.MapFrom(src=>src.Birthday.ToShortDateString()));
            
            CreateMap<CreateAuthorModel, Author>().ForMember(d=>d.Birthday,opt => opt.MapFrom(src=>src.BirthDay.ToShortDateString()));

        }
    }
}