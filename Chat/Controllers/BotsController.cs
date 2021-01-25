using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Data.Authorization;
using Chat.Data.Dtos;
using Chat.Data.Models;
using Chat.Services;
using Chat.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BotsController : ControllerBase
    {

        private readonly IBotService botService;
        public BotsController(IBotService botService)
        {
            this.botService = botService;
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<Bot>> CreateBot([FromBody] BotCreatingDto dto)
        {
            
            ServiceResponse<Bot> serviceCreateBot = await botService.CreateBotAsync(dto);
            if (serviceCreateBot.Success == false)
            {
                return BadRequest(serviceCreateBot.Message);
            }
            return Ok(serviceCreateBot);
        }
    }
}
