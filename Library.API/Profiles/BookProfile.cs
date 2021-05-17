using AutoMapper;
using Library.API.Models;
using Book = Library.API.Entities.Book;

namespace Library.API.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookWithConcatenatedAuthorName>()
                .ForMember(
                    dest => dest.Author,
                    opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"));

            CreateMap<Book, Models.Book>()
                .ForMember(dest => dest.AuthorFirstName, opt => opt.MapFrom(src => $"{src.Author.FirstName}"))
                .ForMember(dest => dest.AuthorLastName, opt => opt.MapFrom(src => $"{src.Author.LastName}"));

            CreateMap<BookForCreation, Book>();
            CreateMap<BookForCreationWithAmountOfPages, Book>();
        }
    }
}