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

        public OpenChestService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
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
            var skills = await _dataContext.Skills.Where(s => randomResult.Contains(s.Id)).ToListAsync();

            var response = new ServiceResponse<List<GetSkillDto>>
            {
                Data = skills.Select(item => _mapper.Map<GetSkillDto>(item)).ToList()
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

            // Query the Weapons to retrieve rows with matching IDs
            var weapons = await _dataContext.Weapons.Where(w => randomResult.Contains(w.Id)).ToListAsync();

            var response = new ServiceResponse<List<GetWeaponDto>>
            {
                Data = weapons.Select(item => _mapper.Map<GetWeaponDto>(item)).ToList()
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