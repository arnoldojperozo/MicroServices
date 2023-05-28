using PlatformService.Data.Interfaces;
using PlatformService.Models;

namespace PlatformService.Data;

public class PlatformRepo : IPlatformRepo
{
    private readonly AppDbContext _appDbContext;

    public PlatformRepo(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void CreatePlatform(Platform platform)
    {
        if(platform != null){
            _appDbContext.Platforms.Add(platform);
        }
        else
            throw new ArgumentNullException(nameof(platform));
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return _appDbContext.Platforms.ToList();
    }

    public Platform GetPlatformById(int id)
    {
        return _appDbContext.Platforms.FirstOrDefault(x => x.Id == id);
    }

    public bool SaveChanges()
    {
        return (_appDbContext.SaveChanges() >= 0);
    }
}