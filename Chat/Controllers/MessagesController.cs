using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Data.Authorization;
using Chat.Data.Dtos;
using Chat.Data.Models;
using Chat.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService messageService;
        public MessagesController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Message>> SendMessage([FromBody] MessageSendingDto dto)
        {
            User usr = HttpContext.Items["User"] as User;
            var serviceSendMessage = await messageService.SendMessageAsync(dto, usr.Id);
            if (serviceSendMessage.Success == false)
            {
                return BadRequest(serviceSendMessage.Message);
            }
            return Ok(serviceSendMessage);
        }
        [HttpDelete("[action]")]
        public async Task<ActionResult<Message>> DeleteMessage([FromBody] MessageDeletingDto dto)
        {
            User usr = HttpContext.Items["User"] as User;
            var serviceDeleteMessage = await messageService.DeleteMessageAsync(dto, usr.Id);
            if (serviceDeleteMessage.Success == false)
            {
                return BadRequest(serviceDeleteMessage.Message);
            }
            return Ok(serviceDeleteMessage);
        }
        [HttpPut("[action]")]
        public async Task<ActionResult<Message>> EditMessage([FromBody] MessageEditingDto dto)
        {
            User usr = HttpContext.Items["User"] as User;
            var serviceEditMessage = await messageService.EditMessageAsync(dto, usr.Id);
            if (serviceEditMessage.Success == false)
            {
                return BadRequest(serviceEditMessage.Message);
            }
            return Ok(serviceEditMessage);
        }

    }
}
