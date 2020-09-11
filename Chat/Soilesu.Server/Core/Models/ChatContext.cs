using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soilesu.Server.Core.Models
{
   public class ChatContext : DbContext
    {
        public ChatContext() : base("MyConnection")
        {

        }
       public DbSet<User> Users { get; set; }
       public DbSet<Cookie> Cookies { get; set; }
    }
}
