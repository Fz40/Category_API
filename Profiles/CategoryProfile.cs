using AutoMapper;
using API.Dtos;
using API.Models;

namespace API.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            // * CreateMap<Source , Target>
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryUpdateDto>();

        }
    }
}