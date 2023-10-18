using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.Data;
using elemental_heroes_server.DTOs.SkillDtos;
using elemental_heroes_server.DTOs.WeaponDtos;

namespace elemental_heroes_server.Services.OpenChestService
{
    public class OpenChestService : IOpenChestService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OpenChestService(DataContext dataContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<List<GetSkillDto>>> OpenSkillChest()
        {
            // Get the number of skills in total
            var count = await _dataContext.Skills.CountAsync();

            // Config this: Number of Items get per chest
            int itemCount = 3;

            // Get random
            List<int> randomResult = GenerateRandom(itemCount, 1, count);

            // Query the Skills to retrieve rows with matching IDs
            var obtainedSkills = await _dataContext.Skills.Where(s => randomResult.Contains(s.Id)).ToListAsync();

            // Get the authed UserId
            int userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            // Get the User
            var user = await _dataContext.Users.Include(u => u.Skills).FirstOrDefaultAsync(u => u.Id == userId);

            // Get the list of skill owned
            List<Skill>? ownedSkills = user!.Skills;

            // If ownedSkills is null
            if (ownedSkills is null)
            {
                user.Skills!.AddRange(obtainedSkills);
            }
            // Else check for duplicates before adding
            else
            {
                foreach (Skill obtainedSkill in obtainedSkills)
                {
                    int index = ownedSkills.FindIndex(item => item.Id == obtainedSkill.Id);
                    if (index >= 0)
                    {
                        // Element exists then skip
                        continue;
                    }
                    else
                    {
                        ownedSkills.Add(obtainedSkill);
                    }
                }
            }

            // Save changes
            await _dataContext.SaveChangesAsync();


            var response = new ServiceResponse<List<GetSkillDto>>
            {
                Data = obtainedSkills.Select(item => _mapper.Map<GetSkillDto>(item)).ToList()
            };

            return response;
        }

        public async Task<ServiceResponse<List<GetWeaponDto>>> OpenWeaponChest()
        {
            // Get the number of skills in total
            var count = await _dataContext.Weapons.CountAsync();

            // Config this: Number of Items get per chest
            int itemCount = 1;

            // Get random
            List<int> randomResult = GenerateRandom(itemCount, 1, count);

            // Query the Skills to retrieve rows with matching IDs
            var obtainedWeapons = await _dataContext.Weapons.Where(w => randomResult.Contains(w.Id)).ToListAsync();

            // Get the authed UserId
            int userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            // Get the User
            var user = await _dataContext.Users.Include(u => u.Weapons).FirstOrDefaultAsync(u => u.Id == userId);

            // Get the list of skill owned
            List<Weapon>? ownedWeapons = user!.Weapons;

            // If ownedWeapons is null
            if (ownedWeapons is null)
            {
                user.Weapons!.AddRange(obtainedWeapons);
            }
            // Else check for duplicates before adding
            else
            {
                foreach (Weapon obtainedWeapon in obtainedWeapons)
                {
                    int index = ownedWeapons.FindIndex(item => item.Id == obtainedWeapon.Id);
                    if (index >= 0)
                    {
                        // Element exists then skip
                        continue;
                    }
                    else
                    {
                        ownedWeapons.Add(obtainedWeapon);
                    }
                }
            }

            // Save changes
            await _dataContext.SaveChangesAsync();


            var response = new ServiceResponse<List<GetWeaponDto>>
            {
                Data = obtainedWeapons.Select(item => _mapper.Map<GetWeaponDto>(item)).ToList()
            };

            return response;
        }

        // For generating random number
        private List<int> GenerateRandom(int n, int min, int max)
        {
            if (n > (max - min + 1))
            {
                throw new ArgumentException("Cannot generate more unique integers than the range allows.");
            }

            List<int> uniqueRandomIntegers = new List<int>();
            Random random = new Random();

            while (uniqueRandomIntegers.Count < n)
            {
                int randomNumber = random.Next(min, max + 1);

                if (!uniqueRandomIntegers.Contains(randomNumber))
                {
                    uniqueRandomIntegers.Add(randomNumber);
                }
            }

            return uniqueRandomIntegers;
        }
    }
}