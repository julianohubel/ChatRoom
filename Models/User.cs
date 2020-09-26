using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Models
{
    public class User : IdentityUser
    {

        public User()
        {
            Messages = new List<Message>();
        }        
        public List<Message> Messages { get; set; }
    }
}
