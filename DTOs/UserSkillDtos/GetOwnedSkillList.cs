using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.SkillDtos;

namespace elemental_heroes_server.DTOs.UserSkillDtos
{
    public class GetOwnedSkillList
    {
        public int Id { get; set; }
        public int TotalSkillCount { get; set; } = 0;
        public List<GetSkillDto>? Skills { get; set; }
    }
}