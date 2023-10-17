using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TivitProject_BL.EmailSenderManager
{
    public interface IEmailManager
    {
        //from 
        //to
        //CC
        //BCC
        //subject
        //dosya ekle...ilerleyen projelerde yazarız
        bool SendEmail(EmailMessageModel model);

        Task SendMailAsync(EmailMessageModel model);

        bool SendEmailGmail(EmailMessageModel model);

    }
}
