using SoilesuX.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Java.Lang;

namespace SoilesuX.Services
{
    class ApiServices
    {
        private string address = "http://192.168.1.71:15000";
       public async Task<HttpResponseMessage> RegisterAsync(User user)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(user);
                HttpContent content = new StringContent(json);
                try
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync(address+ "/api/auth/sign-up", content);                   
                    return response;
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<HttpResponseMessage> LoginAsync(User user)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(user);
                HttpContent content = new StringContent(json);
                try
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync(address+"/api/auth/sign-in", content);                   
                    return response;
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<HttpResponseMessage> SearchByNameOrEmail(string text)
        {
            using (var client = new HttpClient())
            {                
                HttpContent content = new StringContent(text);
                try
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/html");
                    var response = await client.PostAsync(address+"/search-user", content);
                    return response;
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }
        
    }
}
