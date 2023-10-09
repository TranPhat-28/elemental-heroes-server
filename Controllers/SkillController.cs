using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.DTOs.SkillDtos;
using elemental_heroes_server.Services.SkillService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace elemental_heroes_server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetSkillDto>>>> GetAllSkill()
        {
            var response = await _skillService.GetAllSkills();

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }

        // User cannot perform this action
        // [HttpPost]
        // public async Task<ActionResult<ServiceResponse<GetSkillDto>>> AddSkill(AddSkillDto newSkill)
        // {
        //     var response = await _skillService.AddSkill(newSkill);

        //     if (!response.IsSuccess)
        //     {
        //         return BadRequest(response);
        //     }
        //     else
        //     {
        //         return Ok(response);
        //     }
        // }
    }
}