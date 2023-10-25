using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.GameDtos;

namespace elemental_heroes_server.Services.GameService
{
    public interface IGameService
    {
        // Singleplayer
        Task<ServiceResponse<GetBotHeroDto>> GetBotHeroData();
        // Challenge a friend
    }
}