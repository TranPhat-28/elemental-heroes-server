using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.Data;
using elemental_heroes_server.DTOs.GameDtos;

namespace elemental_heroes_server.Services.GameService
{
    public class GameService : IGameService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public GameService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetBotHeroDto>> GetBotHeroData()
        {
            var response = new ServiceResponse<GetBotHeroDto>();

            try
            {
                // Generate a random number between 1 and 5
                Random random = new Random();
                int randomElement = random.Next(1, 6);
                int randomAttackType = random.Next(1, 3);
                int randomDamageType = random.Next(1, 3);

                // Get random Weapon for the bot
                int randomWeapon = random.Next(1, 11);
                var weapon = await _dataContext.Weapons.FirstOrDefaultAsync(w => w.Id == randomWeapon);
            
            var mockData = new GetBotHeroDto
            {
                Name = GetRandomBotName(),
                Element = (Element)randomElement,
                Hp = GetRandomBotStat(3, 5),
                Attack = GetRandomBotStat(3, 6),
                Defense = GetRandomBotStat(3, 6),
                BonusHp = GetRandomBotStat(3, 6),
                BonusAttack = GetRandomBotStat(3, 6),
                BonusDefense = GetRandomBotStat(3, 6),
                AttackType = (AttackType)randomAttackType,
                DamageType = (DamageType)randomDamageType,
                Weapon = _mapper.Map<GetWeaponDto>(weapon),
            };

                response.Data = mockData;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetSingleplayerMatchResultDto>> GetSingleplayerMatchResult()
        {
            var response = new ServiceResponse<GetSingleplayerMatchResultDto>();

            response.Data = new GetSingleplayerMatchResultDto
            {
                PlayerVictory = true,
            };

            return response;
        }

        private string GetRandomBotName()
        {
            string[] nameList = {
                "Botnaldo",
                "Botzilla",
                "Botman",
                "Botinator",
                "Iron Bot",
                "Bot-achu",
                "Bot Solo",
                "Botbuster",
                "Botpool",
                "Bot Trekker",
                "Sherlock Botmes",
                "Botfury",
                "Botmazing",
                "Bot Max",
                "Botlight",
                "Bot and Furious",
                "Bot Potter",
                "Botwick",
                "Bot Lightyear",
                "Bot Simpson"
            };

            Random random = new Random();
            int index = random.Next(0, nameList.Length);
            return nameList[index];
        }

        private int GetRandomBotStat(int min, int max)
        {
            // Generate a random number between min and max
            Random random = new Random();
            int stat = random.Next(min, max + 1) * 10;
            return stat;
        }
    }
}