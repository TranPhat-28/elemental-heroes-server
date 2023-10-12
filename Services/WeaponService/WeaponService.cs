using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.Data;
using elemental_heroes_server.DTOs.WeaponDtos;

namespace elemental_heroes_server.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public WeaponService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetWeaponDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var response = new ServiceResponse<GetWeaponDto>();
            var newWeaponObj = _mapper.Map<Weapon>(newWeapon);

            try
            {
                // Add the authed UserId to the Obj
                // int userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                // newHeroObj.UserId = userId;

                // Add to the DB and save changes
                _dataContext.Weapons.Add(newWeaponObj);
                await _dataContext.SaveChangesAsync();

                // Return the newly created Weapon
                var createdWeapon = await _dataContext.Weapons.FirstOrDefaultAsync(s => s.Id == newWeaponObj.Id);

                // Response
                response.Data = _mapper.Map<GetWeaponDto>(createdWeapon);
                response.Message = "Weapon added successfully";

            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetWeaponDto>>> GetAllWeapons()
        {
            var response = new ServiceResponse<List<GetWeaponDto>>();

            try
            {
                var dbWeaponList = await _dataContext.Weapons.ToListAsync();
                response.Data = dbWeaponList.Select(item => _mapper.Map<GetWeaponDto>(item)).ToList();
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