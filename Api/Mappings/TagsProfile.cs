using System;
using Api.DTOS;
using Api.EF.Entities;
using AutoMapper;

namespace Api.Mappings
{
    public class TagsProfile : Profile
    {
        public TagsProfile()
        {
            CreateMap<Tags, TagsDto>()
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
