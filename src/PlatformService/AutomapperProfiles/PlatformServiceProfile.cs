using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.AutomapperProfiles;

public class PlatformServiceProfile : Profile
{
    public PlatformServiceProfile()
    {
        //Source => Target
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformCreateDto, Platform>();
    }
}