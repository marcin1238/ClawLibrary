using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClawLibrary.Mail.Models
{
    public class Email
    {
        public string SenderName { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailTemplatePath { get; set; }
        public Dictionary<string, string> ElementsToReplace { get; set; }
        public Dictionary<string, string> Images { get; set; }
    }
}
