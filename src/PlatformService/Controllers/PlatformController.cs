using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Interfaces;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http.Interfaces;

namespace PlatformService.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class PlatformController : ControllerBase
{
    private readonly IPlatformRepo _platformRepo;
    private readonly ICommandDataClient _httpClient;
    private readonly IMapper _mapper;
    
    public PlatformController(IPlatformRepo platformRepo, IMapper mapper, ICommandDataClient httpClient)
    {
        _mapper = mapper;
        _platformRepo = platformRepo;
        _httpClient = httpClient;
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
    public async Task<ActionResult<PlatformReadDto>> GetPlatformById(PlatformCreateDto platformCreateDto)
    {
        var platformModel = _mapper.Map<Platform>(platformCreateDto);

        _platformRepo.CreatePlatform(platformModel);
        _platformRepo.SaveChanges();

        var platformReadDto=_mapper.Map<PlatformReadDto>(platformModel);

        try
        {
            await _httpClient.SendPlatformToCommand(platformReadDto);
        }
        catch(Exception e)
        {
            Console.WriteLine($"--> Could not send Synchronously: {e.Message}");
        }

        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
    }
    #endregion
}