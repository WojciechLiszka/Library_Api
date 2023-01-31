using AutoMapper;
using Library_Api.Entity;
using Library_Api.Models;

namespace Library_Api
{
    public class LibraryMappingProfile : Profile
    {
        public LibraryMappingProfile()
        {
            CreateMap<CreateBookDto, Book>();
        }
    }
}