using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using elemental_heroes_server.Data;
using elemental_heroes_server.DTOs.SkillDtos;
using elemental_heroes_server.DTOs.UserSkillDtos;

namespace elemental_heroes_server.Services.SkillService
{
    public class SkillService : ISkillService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SkillService(DataContext dataContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<ServiceResponse<GetOwnedSkillList>> GetAllSkills()
        {
            var response = new ServiceResponse<GetOwnedSkillList>();

            // Get the authed UserId
            int userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            try
            {
                var userData = await _dataContext.Users.Include(u => u.Skills).FirstOrDefaultAsync(u => u.Id == userId);
                response.Data = _mapper.Map<GetOwnedSkillList>(userData);

                // Number of Skills in total, for rendering
                response.Data.TotalSkillCount = await _dataContext.Skills.CountAsync();
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