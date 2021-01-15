using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Dtos
{
    public class AuthenticateDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Tag { get;  set; }
        [Required]
        public string Name { get; set; }
    }
}
