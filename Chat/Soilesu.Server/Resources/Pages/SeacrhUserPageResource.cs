using Soilesu.Server.Core.Attributes;
using Soilesu.Server.Core.Interfaces;
using Soilesu.Server.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Soilesu.Server.Resources.Pages
{
    [Authorization]
    [ResourceRoute("search-user")]
    class SeacrhUserPageResource : IResourceProvider
    {
        public void Process(HttpListenerRequest request, HttpListenerResponse response)
        {
            var vs = new MemoryStream();
            request.InputStream.CopyTo(vs);
            byte[] bytes = vs.ToArray();
            string body = Encoding.UTF8.GetString(bytes);
            using(var db = new ChatContext())
            {
               if(db.Users.FirstOrDefault(p => p.UserName == body) != null)
                {
                    bytes = Encoding.UTF8.GetBytes("Found");
                    response.OutputStream.Write(bytes, 0, bytes.Length);
                    response.ContentType = "text/html";
                    response.StatusCode = 200;
                    response.Close();
                }
                else
                {
                    
                    response.StatusCode = 404;
                    response.Close();
                }
            }
        }
    }
}
