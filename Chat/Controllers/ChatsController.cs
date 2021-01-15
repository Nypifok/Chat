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
    public class ChatsController : ControllerBase
    {
        private readonly IChatService chatService;
        public ChatsController(IChatService chatService)
        {
            this.chatService = chatService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<IEnumerable<ChatMember>>> AddToChat([FromBody] ChatActionDto dto)
        {
            User usr = HttpContext.Items["User"] as User;
            ServiceResponse<IEnumerable<ChatMember>> serviceAddToChat = await chatService.AddToChatAsync(dto,usr.Id);
            if (serviceAddToChat.Success == false)
            {
                return BadRequest(serviceAddToChat.Message);
            }
            return Ok(serviceAddToChat);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<ServiceResponse<Data.Models.Chat>>> CreateNewChat([FromBody] ChatCreatingDto dto)
        {
            User usr = HttpContext.Items["User"] as User;
            var serviceCreatenewChat = await chatService.CreateNewChatAsync(dto, usr.Id);
            if (serviceCreatenewChat.Success == false)
            {
                return BadRequest(serviceCreatenewChat.Message);
            }
            return Ok(serviceCreatenewChat);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<ServiceResponse<Data.Models.Chat>>> CreateNewDialog([FromBody] DialogCreatingDto dto)
        {
            User usr = HttpContext.Items["User"] as User;
            var serviceCreatenewChat = await chatService.CreateNewDialogAsync(dto, usr.Id);
            if (serviceCreatenewChat.Success == false)
            {
                return BadRequest(serviceCreatenewChat.Message);
            }
            return Ok(serviceCreatenewChat);
        }
        [HttpDelete("[action]")]
        public async Task<ActionResult<IEnumerable<ChatMember>>> DeleteFromChat([FromBody] ChatActionDto dto)
        {
            User usr = HttpContext.Items["User"] as User;
            var serviceDeleteFromChatChat = await chatService.DeleteFromChatAsync(dto, usr.Id);
            if (serviceDeleteFromChatChat.Success == false)
            {
                return BadRequest(serviceDeleteFromChatChat.Message);
            }
            return Ok(serviceDeleteFromChatChat);
        }
    }
}
