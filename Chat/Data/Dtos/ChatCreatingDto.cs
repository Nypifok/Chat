using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Dtos
{
    public class ChatCreatingDto
    {
        [Required]
        [StringLength(30,MinimumLength =1,ErrorMessage = "Maxlength limit exceeded")]
        public string ChatTitle { get; set; }
    }
}
