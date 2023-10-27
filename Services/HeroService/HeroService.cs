using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using elemental_heroes_server.Data;
using elemental_heroes_server.DTOs.HeroDtos;
using elemental_heroes_server.DTOs.WeaponDtos;
using Microsoft.EntityFrameworkCore;

namespace elemental_heroes_server.Services.HeroService
{
    public class HeroService : IHeroService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HeroService(DataContext dataContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<GetHeroDto>> AddHero(AddHeroDto newHero)
        {
            var response = new ServiceResponse<GetHeroDto>();
            var newHeroObj = _mapper.Map<Hero>(newHero);


            if (newHeroObj.Name == "")
            {
                response.IsSuccess = false;
                response.Message = "Missing Hero name";
                return response;
            }
            else if (newHeroObj.Attack + newHeroObj.Defense + newHeroObj.Hp > 100)
            {
                response.IsSuccess = false;
                response.Message = "Invalid stats setup";
                return response;
            }
            else
            {
                try
                {

                    // Add the authed UserId to the Obj
                    int userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                    newHeroObj.UserId = userId;

                    // Add to the DB and save changes
                    _dataContext.Heroes.Add(newHeroObj);
                    await _dataContext.SaveChangesAsync();

                    // Return the newly created Hero
                    var createdHero = await _dataContext.Heroes.FirstOrDefaultAsync(h => h.UserId == userId);

                    // Response
                    response.Data = _mapper.Map<GetHeroDto>(createdHero);
                    response.Message = "Hero created successfully";

                }
                catch (Exception e)
                {
                    response.IsSuccess = false;
                    response.Message = e.Message;
                    Console.WriteLine(e.Message);
                }
            }

            return response;
        }

        public async Task<ServiceResponse<GetHeroDto>> EquipSkill(EquipSkillDto equipSkillDto)
        {
            var response = new ServiceResponse<GetHeroDto>();

            try
            {
                // Authed User ID
                var userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                // Get the User
                var user = await _dataContext.Users.Include(u => u.Skills).FirstOrDefaultAsync(u => u.Id == userId);

                // Get the list of skills owned
                List<Skill>? ownedSkills = user!.Skills;

                // If ownedSkills is null
                if (ownedSkills is null)
                {
                    throw new Exception("You have not collected this skill yet");
                }
                // Else check if user has the skill
                int index = ownedSkills.FindIndex(item => item.Id == equipSkillDto.SkillId);

                if (index >= 0)
                {
                    // Get the skill info
                    var skill = await _dataContext.Skills.FirstOrDefaultAsync(s => s.Id == equipSkillDto.SkillId);
                    // Get the hero
                    var hero = await _dataContext.Heroes.FirstOrDefaultAsync(h => h.UserId == userId);
                    // If no hero yet
                    if (hero is null)
                    {
                        throw new Exception("Create a hero first before equipping skill");
                    }

                    // Equip the weapon to the corresponding slot
                    switch (equipSkillDto.Slot)
                    {
                        // For now, do not allow to equip to an existing slot
                        case 1:
                            if (hero.SkillA != null)
                            {
                                throw new Exception("Please unequip the skill in the slot first");
                            }
                            hero.SkillA = skill;
                            break;
                        case 2:
                            if (hero.SkillB != null)
                            {
                                throw new Exception("Please unequip the skill in the slot first");
                            }
                            hero.SkillB = skill;
                            break;
                        case 3:
                            if (hero.SkillC != null)
                            {
                                throw new Exception("Please unequip the skill in the slot first");
                            }
                            hero.SkillC = skill;
                            break;
                        default:
                            throw new Exception("Invalid skill slot");
                    }

                    // Apply the bonus Element stat
                    if (hero.Element == skill!.Element)
                    {
                        hero.BonusAttack += 10;
                    }

                    await _dataContext.SaveChangesAsync();

                    // Return the newly updated hero
                    response.Data = _mapper.Map<GetHeroDto>(hero);
                    response.Message = "Skill is equipped";
                }
                else
                {
                    throw new Exception("You have not collected this skill yet");
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetHeroDto>> EquipWeapon(EquipWeaponDto equipWeaponDto)
        {
            var response = new ServiceResponse<GetHeroDto>();

            try
            {
                // Authed User ID
                var userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                // Get the User
                var user = await _dataContext.Users.Include(u => u.Weapons).FirstOrDefaultAsync(u => u.Id == userId);

                // Get the list of weapons owned
                List<Weapon>? ownedWeapons = user!.Weapons;

                // If ownedWeapons is null
                if (ownedWeapons is null)
                {
                    throw new Exception("You have not collected this weapon yet");
                }
                // Else check if user has the weapon
                int index = ownedWeapons.FindIndex(item => item.Id == equipWeaponDto.WeaponId);

                if (index >= 0)
                {
                    // Get the weapon info
                    var weapon = await _dataContext.Weapons.FirstOrDefaultAsync(w => w.Id == equipWeaponDto.WeaponId);
                    // Get the hero
                    var hero = await _dataContext.Heroes.FirstOrDefaultAsync(h => h.UserId == userId);
                    // If no hero yet
                    if (hero is null)
                    {
                        throw new Exception("Create a hero first before equipping weapon");
                    }
                    // Equip the weapon
                    hero.Weapon = weapon;
                    // Apply the bonus stat
                    if (hero.Element == weapon!.Element)
                    {
                        hero.BonusHp = 40;
                        hero.BonusDefense = 20;
                    }
                    else
                    {
                        hero.BonusHp = 0;
                        hero.BonusDefense = 0;
                    }

                    await _dataContext.SaveChangesAsync();

                    // Return the newly updated hero
                    response.Data = _mapper.Map<GetHeroDto>(hero);
                    response.Message = "Weapon is equipped";
                }
                else
                {
                    throw new Exception("You have not collected this weapon yet");
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetHeroDto>> GetHero()
        {
            var response = new ServiceResponse<GetHeroDto>();

            try
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var hero = await _dataContext.Heroes.Include(h => h.Weapon).Include(h => h.SkillA).Include(h => h.SkillB).Include(h => h.SkillC).FirstOrDefaultAsync(h => h.UserId == userId);

                if (hero is null)
                {
                    response.Message = "No hero found";
                }
                else
                {
                    response.Data = _mapper.Map<GetHeroDto>(hero);
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetHeroDto>> RemoveSkill(RemoveSkillDto removeSkillDto)
        {
            var response = new ServiceResponse<GetHeroDto>();

            try
            {
                // Authed User ID
                var userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                // Get the hero
                var hero = await _dataContext.Heroes.Include(h => h.SkillA).Include(h => h.SkillB).Include(h => h.SkillC).FirstOrDefaultAsync(h => h.UserId == userId);

                // If hero is null
                if (hero is null)
                {
                    throw new Exception("Create a hero first before unequipping skill");
                }

                // Remove the skill and unapply the bonus stat
                switch (removeSkillDto.Slot)
                {
                    case 1:
                        if (hero.SkillA != null)
                        {
                            if (hero.Element == hero.SkillA.Element)
                            {
                                hero.BonusAttack -= 10;
                            }
                            hero.SkillA = null;
                        }
                        break;
                    case 2:
                        if (hero.SkillB != null)
                        {
                            if (hero.Element == hero.SkillB.Element)
                            {
                                hero.BonusAttack -= 10;
                            }
                            hero.SkillB = null;
                        }
                        break;
                    case 3:
                        if (hero.SkillC != null)
                        {
                            if (hero.Element == hero.SkillC.Element)
                            {
                                hero.BonusAttack -= 10;
                            }
                            hero.SkillC = null;
                        }
                        break;
                    default:
                        throw new Exception("Invalid skill slot");
                }
                await _dataContext.SaveChangesAsync();

                // Return the newly updated hero
                response.Data = _mapper.Map<GetHeroDto>(hero);
                response.Message = "Skill is unequipped";
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetHeroDto>> RemoveWeapon()
        {
            var response = new ServiceResponse<GetHeroDto>();

            try
            {
                // Authed User ID
                var userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                // Get the hero
                var hero = await _dataContext.Heroes.Include(h => h.Weapon).FirstOrDefaultAsync(h => h.UserId == userId);

                // If hero is null
                if (hero is null)
                {
                    throw new Exception("Create a hero first before unequipping weapon");
                }

                // Remove the weapon
                hero.Weapon = null;
                await _dataContext.SaveChangesAsync();

                // Remove bonus stats
                hero.BonusHp = 0;
                hero.BonusDefense = 0;

                // Return the newly updated hero
                response.Data = _mapper.Map<GetHeroDto>(hero);
                response.Message = "Weapon is unequipped";
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetHeroDto>> UpdateHero(UpdateHeroDto updateHero)
        {
            var response = new ServiceResponse<GetHeroDto>();

            try
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var hero = await _dataContext.Heroes.FirstOrDefaultAsync(h => h.UserId == userId);
                if (hero is null)
                {
                    throw new Exception("Cannot find hero");
                }

                hero.Name = updateHero.Name;
                hero.Element = updateHero.Element;
                hero.Hp = updateHero.Hp;
                hero.Attack = updateHero.Attack;
                hero.Defense = updateHero.Defense;
                hero.AttackType = updateHero.AttackType;
                hero.DamageType = updateHero.DamageType;

                // Save the changes
                await _dataContext.SaveChangesAsync();

                // Return the newly updated hero
                response.Data = _mapper.Map<GetHeroDto>(hero);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetHeroDto>> UpdateHeroName(UpdateHeroNameDto updateHeroName)
        {
            var response = new ServiceResponse<GetHeroDto>();

            try
            {
                int userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var hero = await _dataContext.Heroes.FirstOrDefaultAsync(h => h.UserId == userId);
                if (hero is null)
                {
                    throw new Exception("Cannot find hero");
                }
                else if (updateHeroName.Name == "")
                {
                    throw new Exception("Hero name must not be empty");
                }

                hero.Name = updateHeroName.Name;

                // Save the changes
                await _dataContext.SaveChangesAsync();

                // Return the newly updated hero
                response.Data = _mapper.Map<GetHeroDto>(hero);
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