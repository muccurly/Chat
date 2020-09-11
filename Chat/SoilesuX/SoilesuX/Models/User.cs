using System;
using System.Collections.Generic;
using System.Text;

namespace SoilesuX.Model
{
  public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public bool Verified { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
