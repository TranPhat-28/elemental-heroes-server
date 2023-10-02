using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.HeroDtos;

namespace elemental_heroes_server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Hero, GetHeroDto>();
            CreateMap<AddHeroDto, Hero>();   
        }
    }
}