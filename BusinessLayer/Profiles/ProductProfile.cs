using AutoMapper;
using BusinessLayer.Models.Auth;
using BusinessLayer.Models.Product;
using DataAccessLayer.Entities;

namespace BusinessLayer.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductRequest>().ReverseMap();
            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<Product, ProductUpdateRequest>().ReverseMap();

            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<User, UserRegisterRequest>().ReverseMap();

        }
    }
}
