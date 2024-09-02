using AutoMapper;
using BusinessLayer.Models;
using DataAccessLayer.Entities;

namespace BusinessLayer.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap(); 
            
            CreateMap<User, CreateUserDto>().ReverseMap();

        }
    }
}
