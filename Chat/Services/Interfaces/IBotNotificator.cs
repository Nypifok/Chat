using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services.Interfaces
{
    public interface IBotNotificator
    {
        Task Notificate(Message message);
    }
}
