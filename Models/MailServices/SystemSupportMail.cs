using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMMS.Models.MailServices
{
    public class SystemSupportMail : MasterMailServices
    {
        public SystemSupportMail()
        {
            _senderEmail = "sistemaproyecto2@gmail.com";
            //_passwordEmail = "AdministradorSistema";
            _passwordEmail = "heqeruheadtllrkc";
            _host = "smtp.gmail.com";
            _port = 587;
            _ssl = true;
            initializeSmtpClient();
        }

    }
}
