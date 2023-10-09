using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.SkillDtos;

namespace elemental_heroes_server.Services.SkillService
{
    public interface ISkillService
    {
        Task<ServiceResponse<List<GetSkillDto>>> GetAllSkills();
        Task<ServiceResponse<GetSkillDto>> AddSkill(AddSkillDto newSkill);
    }
}