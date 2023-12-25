using AutoMapper;
using MambaManyToManyCrud.DTOs.MemberDtos;
using MambaManyToManyCrud.DTOs.ProfessionDtos;
using MambaManyToManyCrud.Entities;

namespace MambaManyToManyCrud.MappingProfile
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<ProfesionUpdateDto,Profession>().ReverseMap();
            CreateMap<ProfessionCreateDto, Profession>().ReverseMap();
            CreateMap<ProfessionGetDto, Profession>().ReverseMap();

            CreateMap<MemberGetDto, Member>().ReverseMap();
            CreateMap<MemberCreateDto, Member>().ReverseMap();
            CreateMap<MemberUpdateDto, Member>().ReverseMap();
        }
    }
}
