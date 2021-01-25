using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Dtos
{
    public class BotCreatingDto
    {
        [Required]
        public string Tilte { get; set; }
        public IEnumerable<TypeOfBot> Types { get; set; }
    }
}
