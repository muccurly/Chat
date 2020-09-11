using Soilesu.Server.Core.Attributes;
using Soilesu.Server.Core.Interfaces;
using Soilesu.Server.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Soilesu.Server.Resources.Pages
{
    
   public class ConfirmEmailPageResource : IResourceProvider
    {      
        public delegate void MethodContainer(string route);
        public event MethodContainer onRemove;
        public void Process(HttpListenerRequest request, HttpListenerResponse response)
        {
          string token = request.Url.AbsolutePath.Substring(16);
          using(var db= new ChatContext())
            {
              var user = db.Users.FirstOrDefault(p => p.Token == token);
                user.Token = null;
                user.Verified = true;
                db.SaveChanges();
           }
          onRemove("/api/activation/" + token);
          byte[] bytes = Encoding.UTF8.GetBytes("Your Email has been confirmed!");
            response.OutputStream.Write(bytes, 0, bytes.Length);
            response.ContentType = "text/html";
            response.StatusCode = 200;
            response.Close();
        }        
        public ConfirmEmailPageResource()
        {          
        }
    }
}
