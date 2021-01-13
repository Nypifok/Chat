using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            ServiceResponse<IEnumerable<ChatMember>> serviceAddToChat = await chatService.AddToChatAsync(dto,Guid.Parse("1b68d99c-e54f-475b-a6a1-5af89e0b388b"));
            if (serviceAddToChat.Success == false)
            {
                return BadRequest(serviceAddToChat.Message);
            }
            return Ok(serviceAddToChat);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<ServiceResponse<Data.Models.Chat>>> CreateNewChat([FromBody] ChatCreatingDto dto)
        {
            var serviceCreatenewChat = await chatService.CreateNewChatAsync(dto, Guid.Parse("1b68d99c-e54f-475b-a6a1-5af89e0b388b"));
            if (serviceCreatenewChat.Success == false)
            {
                return BadRequest(serviceCreatenewChat.Message);
            }
            return Ok(serviceCreatenewChat);
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<ServiceResponse<Data.Models.Chat>>> CreateNewDialog([FromBody] DialogCreatingDto dto)
        {
            var serviceCreatenewChat = await chatService.CreateNewDialogAsync(dto, Guid.Parse("1b68d99c-e54f-475b-a6a1-5af89e0b388b"));
            if (serviceCreatenewChat.Success == false)
            {
                return BadRequest(serviceCreatenewChat.Message);
            }
            return Ok(serviceCreatenewChat);
        }
        [HttpDelete("[action]")]
        public async Task<ActionResult<IEnumerable<ChatMember>>> DeleteFromChat([FromBody] ChatActionDto dto)
        {
            var serviceDeleteFromChatChat = await chatService.DeleteFromChatAsync(dto, Guid.Parse("1b68d99c-e54f-475b-a6a1-5af89e0b388b"));
            if (serviceDeleteFromChatChat.Success == false)
            {
                return BadRequest(serviceDeleteFromChatChat.Message);
            }
            return Ok(serviceDeleteFromChatChat);
        }
    }
}
