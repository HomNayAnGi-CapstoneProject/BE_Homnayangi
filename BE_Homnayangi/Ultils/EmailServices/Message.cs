using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Ultils.EmailServices
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IFormFileCollection Attachments { get; set; }

        public Message(
            IEnumerable<string> to,
            string subject,
            string content,
            IFormFileCollection? attachments = null)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress("recipent", x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}

