using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Models
{
    public class Message
    {
        public Message()
        {
            Date = DateTime.Now;
        }
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string  Text { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public User Sender { get; set; }
    }
}
