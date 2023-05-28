using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Data.Interfaces;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class PlatformController : ControllerBase
{
    private readonly IPlatformRepo _platformRepo;
    private readonly IMapper _mapper;
    public PlatformController(IPlatformRepo platformRepo, IMapper mapper)
    {
        _mapper = mapper;
        _platformRepo = platformRepo;
    }

    #region HTTP GET
    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        Console.WriteLine("--> Getting All Platforms ...");

        var platformItems = _platformRepo.GetAllPlatforms();

        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
    }
    
    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
        Console.WriteLine($"--> Getting Platform with ID: {id}  ...");

        var platformItem = _platformRepo.GetPlatformById(id);

        if(platformItem != null)
            return Ok(_mapper.Map<PlatformReadDto>(platformItem));
        else
            return NotFound();
    }
    #endregion

    #region HTTP POST
    [HttpPost]
    public ActionResult<PlatformReadDto> GetPlatformById(PlatformCreateDto platformCreateDto)
    {
        var platformModel = _mapper.Map<Platform>(platformCreateDto);

        _platformRepo.CreatePlatform(platformModel);
        _platformRepo.SaveChanges();

        var platformReadDto=_mapper.Map<PlatformReadDto>(platformModel);

        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
    }
    #endregion
}