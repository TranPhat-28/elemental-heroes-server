using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.HeroDtos;
using elemental_heroes_server.DTOs.SkillDtos;

namespace elemental_heroes_server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateMap<FromSource, ToDestination>
            CreateMap<Hero, GetHeroDto>();
            CreateMap<AddHeroDto, Hero>();
            CreateMap<GetHeroDto, Hero>();

            CreateMap<AddSkillDto, Skill>();
            CreateMap<Skill, GetSkillDto>();
        }
    }
}