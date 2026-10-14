using AutoMapper;
using Domains.Models;
using LapShop.Dto;
using LapShop.ViewModels;

namespace LapShop.Profiles
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            // تعريف التحويل من وإلى بين Models و ViewModels
            CreateMap<TbCategory, CategoryViewModel>().ReverseMap();
            CreateMap<TbItem, VmItemDetails>().ReverseMap();
            CreateMap<CreateCategoryDto, TbCategory>();
            CreateMap<UpdateCategoryDto, TbCategory>();
            CreateMap<CreateItemDto, TbItem>();
            CreateMap<UpdateItemDto, TbItem>();
            CreateMap<TbItem, ItemDto>();
            CreateMap<TbItem, ItemListDto>();
        }
    }
}
