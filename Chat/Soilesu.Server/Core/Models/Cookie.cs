using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soilesu.Server.Core.Models
{
   public class Cookie
    {
        public string CookieID { get; set; }
        public Int64 UserID { get; set; }
        public IEnumerable<User> Users { get; set; }      
            
    }
}
