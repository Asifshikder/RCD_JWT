using System;
using System.Collections.Generic;
using System.Text;

namespace RCD.SERVICE.Interface
{
    public interface IEmailSender
    {
        void Post(string subject, string body, string recipients, string sender);
    }
}
