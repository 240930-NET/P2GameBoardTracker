using AutoMapper;
using P2.API.Model.DTO;

namespace P2.API.Model;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<GameDto, Game>().ReverseMap();
    }
}