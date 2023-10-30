using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.Data;
using elemental_heroes_server.DTOs.HeroDtos;
using elemental_heroes_server.DTOs.SkillDtos;

namespace elemental_heroes_server.Services.GameService
{
    public class GameService : IGameService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GameService(DataContext dataContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<GetBotHeroDto>> GetBotHeroData()
        {
            var response = new ServiceResponse<GetBotHeroDto>();

            try
            {
                // Generate random Element, AttackType and DamageType
                Random random = new Random();
                int randomElement = random.Next(1, 6);
                int randomAttackType = random.Next(1, 3);
                int randomDamageType = random.Next(1, 3);

                // Get random Weapon for the bot
                int randomWeapon = random.Next(1, 11);
                var weapon = await _dataContext.Weapons.FirstOrDefaultAsync(w => w.Id == randomWeapon);

                // Get the number of skills in total
                var skillsCount = await _dataContext.Skills.CountAsync();

                // Get 3 random Skills for the bot
                List<int> randomResult = new List<int>();
                while (randomResult.Count < 3)
                {
                    int randomNumber = random.Next(1, skillsCount + 1);

                    if (!randomResult.Contains(randomNumber))
                    {
                        randomResult.Add(randomNumber);
                    }
                }

                var skillA = await _dataContext.Skills.FirstOrDefaultAsync(s => s.Id == randomResult[0]);
                var skillB = await _dataContext.Skills.FirstOrDefaultAsync(s => s.Id == randomResult[1]);
                var skillC = await _dataContext.Skills.FirstOrDefaultAsync(s => s.Id == randomResult[2]);

                var botData = new GetBotHeroDto
                {
                    Name = GetRandomBotName(),
                    Element = (Element)randomElement,
                    Hp = GetRandomBotStat(3, 5),
                    Attack = GetRandomBotStat(3, 5),
                    Defense = GetRandomBotStat(3, 5),
                    BonusHp = GetRandomBotStat(1, 3),
                    BonusAttack = GetRandomBotStat(1, 3),
                    BonusDefense = GetRandomBotStat(1, 3),
                    AttackType = (AttackType)randomAttackType,
                    DamageType = (DamageType)randomDamageType,
                    Weapon = _mapper.Map<GetWeaponDto>(weapon),
                    SkillA = _mapper.Map<GetSkillDto>(skillA),
                    SkillB = _mapper.Map<GetSkillDto>(skillB),
                    SkillC = _mapper.Map<GetSkillDto>(skillC),
                };

                response.Data = botData;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetSingleplayerMatchResultDto>> GetSingleplayerMatchResult(GetBotHeroDto botHeroData)
        {
            var response = new ServiceResponse<GetSingleplayerMatchResultDto>();

            try
            {
                // Fetch the Hero
                int userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var hero = await _dataContext.Heroes.Include(h => h.Weapon).Include(h => h.SkillA).Include(h => h.SkillB).Include(h => h.SkillC).FirstOrDefaultAsync(h => h.UserId == userId);

                // If null hero
                if (hero is null)
                {
                    throw new Exception("Create a hero before playing");
                }

                // Calculate game result - a list of TurnResult
                List<SingleplayerTurnResult> gameResult = new List<SingleplayerTurnResult>();

                bool gameOver = false;
                int turnCount = 1;

                // Player and Bot stat
                int PlayerHp = hero.Hp + hero.BonusHp;
                int PlayerAtk = hero.Attack + hero.BonusAttack;
                int PlayerDef = hero.Defense + hero.BonusDefense;

                int BotHp = botHeroData.Hp + botHeroData.BonusHp;
                int BotAtk = botHeroData.Attack + botHeroData.BonusAttack;
                int BotDef = botHeroData.Defense + botHeroData.BonusDefense;

                bool PlayerVictory = false;

                // Loop to calculate result
                // In singleplayer, Player always go first
                while (!gameOver)
                {
                    SingleplayerTurnResult turnResult = new SingleplayerTurnResult();

                    // Turn no
                    turnResult.TurnNo = turnCount;

                    // Init HP
                    turnResult.PlayerInitHp = PlayerHp;
                    turnResult.BotInitHp = BotHp;

                    // Damage dealt
                    int playerDealt;
                    int botDealt;

                    switch (turnCount)
                    {
                        case 1:
                            // Skill A

                            // Player Damage
                            if (hero.SkillA is not null)
                            {
                                playerDealt = PlayerAtk * hero.SkillA.Damage / 100 - BotDef;
                            }
                            else
                            {
                                playerDealt = PlayerAtk - BotDef;
                            }
                            if (playerDealt <= 0)
                            {
                                turnResult.PlayerDamageDealt = 0;
                            }
                            else
                            {
                                turnResult.PlayerDamageDealt = playerDealt;
                            }
                            turnResult.BotRemainingHp = turnResult.BotInitHp - playerDealt;

                            // Bot Damage
                            if (botHeroData.SkillA is not null)
                            {
                                botDealt = BotAtk * botHeroData.SkillA.Damage / 100 - PlayerDef;
                            }
                            else
                            {
                                botDealt = BotAtk - PlayerDef;
                            }
                            if (botDealt <= 0)
                            {
                                turnResult.BotDamageDealt = 0;
                            }
                            else
                            {
                                turnResult.BotDamageDealt = botDealt;
                            }
                            turnResult.PlayerRemainingHp = turnResult.PlayerInitHp - botDealt;

                            break;
                        case 2:
                            // Skill B

                            // Player Damage
                            if (hero.SkillB is not null)
                            {
                                playerDealt = PlayerAtk * hero.SkillB.Damage / 100 - BotDef;
                            }
                            else
                            {
                                playerDealt = PlayerAtk - BotDef;
                            }
                            if (playerDealt <= 0)
                            {
                                turnResult.PlayerDamageDealt = 0;
                            }
                            else
                            {
                                turnResult.PlayerDamageDealt = playerDealt;
                            }
                            turnResult.BotRemainingHp = turnResult.BotInitHp - playerDealt;

                            // Bot Damage
                            if (botHeroData.SkillB is not null)
                            {
                                botDealt = BotAtk * botHeroData.SkillB.Damage / 100 - PlayerDef;
                            }
                            else
                            {
                                botDealt = BotAtk - PlayerDef;
                            }
                            if (botDealt <= 0)
                            {
                                turnResult.BotDamageDealt = 0;
                            }
                            else
                            {
                                turnResult.BotDamageDealt = botDealt;
                            }
                            turnResult.PlayerRemainingHp = turnResult.PlayerInitHp - botDealt;

                            break;
                        case 3:
                            // Skill C

                            // Player Damage
                            if (hero.SkillC is not null)
                            {
                                playerDealt = PlayerAtk * hero.SkillC.Damage / 100 - BotDef;
                            }
                            else
                            {
                                playerDealt = PlayerAtk - BotDef;
                            }
                            if (playerDealt <= 0)
                            {
                                turnResult.PlayerDamageDealt = 0;
                            }
                            else
                            {
                                turnResult.PlayerDamageDealt = playerDealt;
                            }
                            turnResult.BotRemainingHp = turnResult.BotInitHp - playerDealt;

                            // Bot Damage
                            if (botHeroData.SkillC is not null)
                            {
                                botDealt = BotAtk * botHeroData.SkillC.Damage / 100 - PlayerDef;
                            }
                            else
                            {
                                botDealt = BotAtk - PlayerDef;
                            }
                            if (botDealt <= 0)
                            {
                                turnResult.BotDamageDealt = 0;
                            }
                            else
                            {
                                turnResult.BotDamageDealt = botDealt;
                            }
                            turnResult.PlayerRemainingHp = turnResult.PlayerInitHp - botDealt;

                            break;
                        default:
                            // Default attack

                            // Player Damage
                            playerDealt = PlayerAtk - BotDef;
                            if (playerDealt <= 0)
                            {
                                turnResult.PlayerDamageDealt = 0;
                            }
                            else
                            {
                                turnResult.PlayerDamageDealt = playerDealt;
                            }
                            turnResult.BotRemainingHp = turnResult.BotInitHp - playerDealt;

                            // Bot Damage
                            botDealt = BotAtk - PlayerDef;
                            if (botDealt <= 0)
                            {
                                turnResult.BotDamageDealt = 0;
                            }
                            else
                            {
                                turnResult.BotDamageDealt = botDealt;
                            }
                            turnResult.PlayerRemainingHp = turnResult.PlayerInitHp - botDealt;

                            break;
                    }

                    // Push the result to the list
                    gameResult.Add(turnResult);

                    // Update HP
                    PlayerHp = turnResult.PlayerRemainingHp;
                    BotHp = turnResult.BotRemainingHp;

                    // Check gameOver
                    if (PlayerHp <= 0)
                    {
                        PlayerVictory = false;
                        gameOver = true;
                    }
                    else if (BotHp <= 0)
                    {
                        PlayerVictory = true;
                        gameOver = true;
                    }

                    // Next turn
                    turnCount++;
                }

                // Calculate the reward
                int reward;
                if (PlayerVictory){
                    reward = 300;
                }
                else {
                    reward = 100;
                }

                // Add the reward
                var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
                user!.Balance += reward;

                // Finish the response
                response.Data = new GetSingleplayerMatchResultDto
                {
                    UserHeroData = _mapper.Map<GetHeroDto>(hero),
                    BotData = botHeroData,
                    PlayerVictory = PlayerVictory,
                    GameResult = gameResult,
                    Reward = reward
                };
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }

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