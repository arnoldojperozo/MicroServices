using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http.Interfaces;

public interface ICommandDataClient
{
    Task SendPlatformToCommand(PlatformReadDto platform);
}