﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Strike.Models
{
    public class MessageSender
    {
        public MessageSender()
        {

        }

        public MessageSender(int messageId, int userId)
        {
            this.MessageId = messageId;
            this.UserId = userId;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MessageId { get; set; }
        public int UserId { get; set; }

        public Message Message { get; set; }
        public User User { get; set; }
    }
}
