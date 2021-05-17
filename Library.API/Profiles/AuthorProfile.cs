using AutoMapper;
using Library.API.Models;
using Author = Library.API.Entities.Author;

namespace Library.API.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, Models.Author>();
            CreateMap<AuthorForUpdate, Author>();
        }
    }
}