using Application.DTOs.Category;
using Application.DTOs.Vendor;
using AutoMapper;
using Domain.Dtos.ApplicationUser;
using Domain.Dtos.Product;
using Domain.Entities;

namespace Application.Mapping;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        // Application user mapping.
        CreateMap<ApplicationUser, UserVm>().ReverseMap();
        CreateMap<CreateUserVm, ApplicationUser>().ReverseMap();
        CreateMap<UpdateUserVm, ApplicationUser>().ReverseMap();

        // Vendor mapping.
        CreateMap<Vendor, VendorVm>().ReverseMap();
        CreateMap<CreateVendorVm, VendorVm>().ReverseMap();
        CreateMap<UpdateVendorVm, VendorVm>().ReverseMap();

        // Category mapping.
        CreateMap<Category, CategoryVm>().ReverseMap();
        CreateMap<CreateCategoryVm, Category>().ReverseMap();
        CreateMap<UpdateCategoryVm, Category>().ReverseMap();

        // Product mapping.
        CreateMap<Product, ProductVm>().ReverseMap();
        CreateMap<CreateProductVm, Product>().ReverseMap();
        CreateMap<UpdateProductVm, Product>().ReverseMap();
    }
}
