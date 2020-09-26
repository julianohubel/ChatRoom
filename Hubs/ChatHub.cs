using ChatRoom.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ChatRoom.Hubs
{
    public class ChatHub : Hub
    {

        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("Receive", message);
        }
    }
}
