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
            var serviceSendMessage = await messageService.SendMessageAsync(dto, Guid.Parse("1b68d99c-e54f-475b-a6a1-5af89e0b388b"));
            if (serviceSendMessage.Success == false)
            {
                return BadRequest(serviceSendMessage.Message);
            }
            return Ok(serviceSendMessage);
        }
        [HttpDelete("[action]")]
        public async Task<ActionResult<Message>> DeleteMessage([FromBody] MessageDeletingDto dto)
        {
            var serviceDeleteMessage = await messageService.DeleteMessageAsync(dto, Guid.Parse("1b68d99c-e54f-475b-a6a1-5af89e0b388b"));
            if (serviceDeleteMessage.Success == false)
            {
                return BadRequest(serviceDeleteMessage.Message);
            }
            return Ok(serviceDeleteMessage);
        }
        [HttpPut("[action]")]
        public async Task<ActionResult<Message>> EditMessage([FromBody] MessageEditingDto dto)
        {
            var serviceEditMessage = await messageService.EditMessageAsync(dto, Guid.Parse("1b68d99c-e54f-475b-a6a1-5af89e0b388b"));
            if (serviceEditMessage.Success == false)
            {
                return BadRequest(serviceEditMessage.Message);
            }
            return Ok(serviceEditMessage);
        }

    }
}
