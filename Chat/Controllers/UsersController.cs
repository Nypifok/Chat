using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Data.Dtos;
using Chat.Data.Models;
using Chat.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate(AuthenticateDto model)
        {
            var response = await userService.Authenticate(model);

            if (response.Success == false)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser(AuthenticateDto model)
        {
            var response = await userService.CreateUser(model);

            if (response.Success == false)
                return BadRequest(response);

            return Ok(response);
        }

    }
}
