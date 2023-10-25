using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.GameDtos;

namespace elemental_heroes_server.Services.GameService
{
    public class GameService : IGameService
    {
        public async Task<ServiceResponse<GetBotHeroDto>> GetBotHeroData()
        {
            var response = new ServiceResponse<GetBotHeroDto>();

            // Generate a random number between 1 and 5
            Random random = new Random();
            int randomElementNumber = random.Next(1, 6);

            var mockData = new GetBotHeroDto
            {
                Name = "God of Bot",
                Element = (Element)randomElementNumber,
                Hp = 100,
                Attack = 100,
                Defense = 100,
                BonusHp = 100,
                BonusAttack = 100,
                BonusDefense = 100,
                AttackType = AttackType.Melee,
                DamageType = DamageType.Physical,
                Weapon = null
            };

            response.Data = mockData;

            return response;
        }
    }
}