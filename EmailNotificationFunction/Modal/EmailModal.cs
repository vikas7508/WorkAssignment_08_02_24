using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotificationFunction.Modal
{
    internal class EmailModal
    {
        public string recipientEmail { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
}
