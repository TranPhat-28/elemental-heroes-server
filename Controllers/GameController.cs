using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elemental_heroes_server.Services.GameService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace elemental_heroes_server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("GetBotHeroData")]
        public async Task<ActionResult<ServiceResponse<GetBotHeroDto>>> GetBotHeroData()
        {
            var response = await _gameService.GetBotHeroData();

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpGet("GetSingleplayerMatchResult")]
        public async Task<ActionResult<ServiceResponse<GetSingleplayerMatchResultDto>>> GetSingleplayerMatchResult()
        {
            var response = await _gameService.GetSingleplayerMatchResult();

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }
    }
}