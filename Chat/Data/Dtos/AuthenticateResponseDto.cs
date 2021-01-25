using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Dtos
{
    public class AuthenticateResponseDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Tag { get; set; }
        [Required]
        public string Token { get; set; }

    }
}
