using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMMS.Models
{
    public class UserAccountModel
    {
        public string Username { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Msgwelcome { get; set; }
        public byte[] Profile { get; set; }
    }
}
