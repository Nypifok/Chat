using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Dtos
{
    public class AuthenticateResponseDto
    {
        public Guid Id { get; set; }
        
        public string Tag { get; set; }
        public string Token { get; set; }

    }
}
