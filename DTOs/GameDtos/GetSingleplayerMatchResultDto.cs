using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.HeroDtos;

namespace elemental_heroes_server.DTOs.GameDtos
{
    public class GetSingleplayerMatchResultDto
    {
        public GetHeroDto UserHeroData { get; set; } = new GetHeroDto();
        public GetBotHeroDto BotData { get; set; } = new GetBotHeroDto();
        public bool PlayerVictory { get; set; }
        public int Reward { get; set; } = 0;
        public List<SingleplayerTurnResult> GameResult { get; set; } = new List<SingleplayerTurnResult>();
    }
}