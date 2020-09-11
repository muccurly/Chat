using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using FastDeepCloner;
using Newtonsoft.Json;
using Soilesu.Server.Core.Attributes;
using Soilesu.Server.Core.Interfaces;
using Soilesu.Server.Core.Models;

namespace Soilesu.Server.Resources.Pages
{
    [ResourceRoute("api/auth/sign-up")]
    public class RegisterPageResource : IResourceProvider
    {
        public delegate void MethodContainer(string route);
        public event MethodContainer onAdd;
        public void Process(HttpListenerRequest request, HttpListenerResponse response)
        {
            var ms = new MemoryStream();
            request.InputStream.CopyTo(ms);
            byte[] bytes = ms.ToArray();
            string body = Encoding.UTF8.GetString(bytes);
            User user = JsonConvert.DeserializeObject<User>(body);         
            using (var db = new ChatContext())
            {
                var checkUser = db.Users.FirstOrDefault(p => p.Email == user.Email);
                if (checkUser != null)
                {
                    bytes = Encoding.UTF8.GetBytes("Email has already registered");
                    response.OutputStream.Write(bytes, 0, bytes.Length);
                    response.ContentType = "text/html";
                    response.StatusCode = 200;
                    response.Close();
                }
                else
                {
                    string token = Guid.NewGuid().ToString();
                    user.Token = token;
                    db.Users.Add(user);
                    onAdd("/api/activation/" + token);
                    SendConfirmEmail(user.Email, "http://192.168.16.1:15000/api/activation/"+token);                   
                    bytes = Encoding.UTF8.GetBytes("Success registered");
                    db.SaveChanges();
                    response.OutputStream.Write(bytes, 0, bytes.Length);
                    response.ContentType = "text/html";
                    response.StatusCode = 200;
                    response.Close();
                }
            }
        }
     
        #region SendEmail
       internal void SendConfirmEmail(string Email, string htmlString)
        {           
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("mirassui18@gmail.com");
                message.To.Add(new MailAddress(Email));
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString  ;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for mail.ru host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("mirassui18@gmail.com", "M13181612");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                smtp.Dispose();
                       
            }
            catch (Exception ex)
            {
               var eror = ex.InnerException.Message;
               
            }
        }
        #region Html

        private string getHtml(User user)
        {
            try
            {
                string messageBody = "<font>The following are the records: </font><br><br>";
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style=\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";
                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "Student Name" + htmlTdEnd;
                messageBody += htmlTdStart + "DOB" + htmlTdEnd;
                messageBody += htmlTdStart + "Email" + htmlTdEnd;
                messageBody += htmlTdStart + "Mobile" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;
                //Loop all the rows from grid vew and added to html td                 
                messageBody = messageBody + htmlTrStart;
                messageBody = messageBody + htmlTdStart + user.UserName + htmlTdEnd; //adding student name  
                messageBody = messageBody + htmlTdStart + user.DateCreated + htmlTdEnd; //adding DOB  
                messageBody = messageBody + htmlTdStart + user.Email + htmlTdEnd; //adding Email                  
                messageBody = messageBody + htmlTrEnd;

                messageBody = messageBody + htmlTableEnd;
                return messageBody; // return HTML Table as string from this function  
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #endregion
    }
}
