﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Dtos
{
    public class MessageEditingDto
    {
        public Guid MessageId { get; set; }
        [Required]
        public string MessageContent { get; set; }
    }
}
