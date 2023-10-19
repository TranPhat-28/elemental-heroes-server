using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.SkillDtos;

namespace elemental_heroes_server.DTOs.ChestDtos
{
    public class GetSkillChestDto
    {
        public List<GetSkillDto>? ObtainedSkills { get; set; }
        public int RemainingBalance { get; set; }
    }
}