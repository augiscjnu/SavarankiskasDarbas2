using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavarankiskasDarbas2.Core.Models
{
    public class StandardUser : User
    {
        public StandardUser()
        {
            Role = "StandardUser";  
        }
    }
}
