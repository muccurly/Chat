﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soilesu.Server.Core.Models
{
   public class User
    {
        [Key]
        public Int64 UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public bool Verified { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
