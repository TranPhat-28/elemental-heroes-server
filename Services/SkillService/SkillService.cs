using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.Data;
using elemental_heroes_server.DTOs.SkillDtos;
using Microsoft.EntityFrameworkCore;

namespace elemental_heroes_server.Services.SkillService
{
    public class SkillService : ISkillService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public SkillService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetSkillDto>> AddSkill(AddSkillDto newSkill)
        {
            var response = new ServiceResponse<GetSkillDto>();
            var newSkillObj = _mapper.Map<Skill>(newSkill);


            try
            {
                // Add the authed UserId to the Obj
                // int userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                // newHeroObj.UserId = userId;

                // Add to the DB and save changes
                _dataContext.Skills.Add(newSkillObj);
                await _dataContext.SaveChangesAsync();

                // Return the newly created Skill
                var createdSkill = await _dataContext.Skills.FirstOrDefaultAsync(s => s.Id == newSkillObj.Id);

                // Response
                response.Data = _mapper.Map<GetSkillDto>(createdSkill);
                response.Message = "Skill added successfully";

            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetSkillDto>>> GetAllSkills()
        {
            var response = new ServiceResponse<List<GetSkillDto>>();

            try
            {
                var dbSkillList = await _dataContext.Skills.ToListAsync();
                response.Data = dbSkillList.Select(item => _mapper.Map<GetSkillDto>(item)).ToList();
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }

            return response;
        }
    }
}