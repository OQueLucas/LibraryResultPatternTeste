using AutoMapper;
using Library.Communication.Requests;
using Library.Communication.Responses;
using Library.Domain.Entities;

namespace Library.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToResponse();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestBook, Book>();
        CreateMap<RequestCategory, Category>();
    }

    private void DomainToResponse()
    {
        CreateMap<Book, ResponseBook>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
        CreateMap<Category, ResponseCategory>();
    }
}
