using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Strike.Models
{
    public class User
    {
        public static string UserId = "UserId";

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string Phone { get; set; }


        //FK for advertisements
        public List<Advertisement> Advertisements { get; set; }

        //FK for send and recived messages

        public List<MessageSender> SentMessages { get; set; }
        public List<MessageReceiver> ReceivedMessages { get; set; }
    }
}
