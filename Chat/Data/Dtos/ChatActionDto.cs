using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Dtos
{
    public class ChatActionDto
    {
        public IEnumerable<User> TargetUsers { get; set; }
        public Guid TargetChatId { get; set; }
    }
}
