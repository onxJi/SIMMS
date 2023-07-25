using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMMS.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Position { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public byte[] Profile { get; set; }
    }
}
