using System;
using System.Collections;
using System.Collections.Generic;

namespace Chat.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Tag { get; set; }
        public string Name { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<ChatMember> ChatMembers { get; set; }
        public User()
        {
            Messages = new List<Message>();
            ChatMembers = new List<ChatMember>();
        }
    }
}