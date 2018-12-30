using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Strike.Models
{
    public class Message
    {
        public Message()
        {

        }

        public Message(string subject, string text)
        {
            this.Subject = subject;
            this.Text = text;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy HH\\:mm}")]
        public DateTime Created { get; set; }
        public bool IsRead { get; set; }

        public MessageSender MessageSender { get; set; }
        public MessageReceiver MessageReceiver { get; set; }
    }
}
