using Newtonsoft.Json;
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
    [ResourceRoute("api/auth/sign-in")]
    class LoginPageResource : IResourceProvider
    {
        public void Process(HttpListenerRequest request, HttpListenerResponse response)
        {
            var ms = new MemoryStream();
            request.InputStream.CopyTo(ms);
            byte[] bytes = ms.ToArray();
            string body = Encoding.UTF8.GetString(bytes);
            User user = JsonConvert.DeserializeObject<User>(body);
            using (var db = new ChatContext())
            {
               User check = db.Users.FirstOrDefault(p=> p.Email==user.Email);
                if (check != null)
                {
                    if (check.Password != user.Password)
                    {
                        bytes = Encoding.UTF8.GetBytes("Wrong Password");
                        response.OutputStream.Write(bytes, 0, bytes.Length);
                        response.ContentType = "text/html";
                        response.StatusCode = 406;                       
                        response.Close();
                        return;
                        
                    }
                    else if (check.Verified == false) {
                        bytes = Encoding.UTF8.GetBytes("Please confirm Email!");
                        response.OutputStream.Write(bytes, 0, bytes.Length);
                        response.ContentType = "text/html";
                        response.StatusCode = 406;
                        response.Close();
                        return;
                    }
                    else
                    {
                        var checkCookie = db.Cookies.FirstOrDefault(p => p.UserID == user.UserID);
                        if ((checkCookie == null))
                        {
                            string guid = Guid.NewGuid().ToString();
                            response.Headers.Add("CookieId", guid);
                            response.Headers.Add("CookieValue", check.UserName);
                            Core.Models.Cookie cookie = new Core.Models.Cookie() { CookieID = guid, UserID = user.UserID };
                            db.Cookies.Add(cookie);
                            bytes = Encoding.UTF8.GetBytes("Successfully");
                            response.OutputStream.Write(bytes, 0, bytes.Length);
                            response.ContentType = "text/html";
                            response.StatusCode = 200;
                            response.Close();
                            return;
                        }
                        else
                        {
                            response.Headers.Add("CookieId", checkCookie.CookieID);
                            response.Headers.Add("CookieValue", check.UserName);
                            bytes = Encoding.UTF8.GetBytes("Successfully");
                            response.OutputStream.Write(bytes, 0, bytes.Length);
                            response.ContentType = "text/html";
                            response.StatusCode = 200;
                            response.Close();
                            return;
                        }
                    }
                }
                else
                {
                    bytes = Encoding.UTF8.GetBytes("Wrong Email");
                    response.OutputStream.Write(bytes, 0, bytes.Length);
                    response.ContentType = "text/html";
                    response.StatusCode = 406;
                response.Close();
                    return;
                }
               
            }

        }
    }
}
