using ManualHttpServer.Extensions;
using Newtonsoft.Json;
using Soilesu.Server.Core.Attributes;
using Soilesu.Server.Core.Interfaces;
using Soilesu.Server.Core.Models;
using Soilesu.Server.Core.Models.Chat;
using Soilesu.Server.Core.WebSocket;
using Soilesu.Server.Repositories;
using Soilesu.Server.Resources.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Soilesu.Server.Core
{
    class HttpServerProvider
    {      
        private readonly HttpListener _httpListener;
        private readonly Uri _address;
        private readonly RoutingProvider _routingProvider;   
        public HttpServerProvider(Uri address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));          
            _httpListener = new HttpListener();
            _routingProvider = new RoutingProvider();
            _address = address;
            _httpListener.Prefixes.Add(_address.ToString());
        }
        public async void StartAsync()
        {            
                ConsoleExtension.WriteLine(
               $"Trying to start HTTP-server at {_address}", ConsoleColor.Yellow);
                _httpListener.Start();         

                ConsoleExtension.WriteLine(
               $"HTTP-server successfully started at {_address}", ConsoleColor.Green);
                while (true)
                {
                var context = await _httpListener.GetContextAsync();                           
                OnRequestHandler(context);                
                ConsoleExtension.WriteLine("Person joined", ConsoleColor.Blue);
                }           
        }     
        private async Task OnRequestHandlerAsync(HttpListenerContext context, IResourceProvider resourceProvider)
        {
           var task = Task.Run(() =>
            {               
                resourceProvider.Process(context.Request,context.Response);
            });
            try
            {
                if ((resourceProvider as RegisterPageResource)!=null)
                {
                    ((RegisterPageResource)resourceProvider).onAdd += _routingProvider.SetResourceProviderByRoute;
                }else if((resourceProvider as ConfirmEmailPageResource) != null)
                {
                    ((ConfirmEmailPageResource)resourceProvider).onRemove += _routingProvider.RemoveResourceProviderByRoute;
                }
                await task;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.Close();
            }

        }
        private void OnRequestHandler(HttpListenerContext context)
        {
            try
            {
                var route = context.Request.RawUrl;
                IResourceProvider resource = _routingProvider.GetResourceProviderByRoute(route);
                var cookie = context.Request.Headers.Get("CookieId") == null ? "" : context.Request.Headers.Get("CookieId");
                var checkAuthorizationAttribute = Attribute.GetCustomAttribute(resource.GetType(), typeof(AuthorizationAttribute));
                if (resource != null && checkAuthorizationAttribute != null)
                {
                    using (var db = new ChatContext())
                    {
                        if (db.Cookies.FirstOrDefault(p=> p.CookieID== cookie)!=null)
                            OnRequestHandlerAsync(context, resource);
                        else
                        {

                            context.Response.StatusCode = 422;
                            byte[] vs = Encoding.UTF8.GetBytes("У вас неверные данные!");
                            context.Response.OutputStream.Write(vs, 0, vs.Length);
                            context.Response.Close();
                        }
                    }
                }
                else
                {
                    if (resource != null)
                    {
                        OnRequestHandlerAsync(context, resource);
                    }
                    else
                    {
                        context.Response.StatusCode = 404;
                        byte[] vs = Encoding.UTF8.GetBytes("404!");
                        context.Response.OutputStream.Write(vs, 0, vs.Length);
                        context.Response.Close();
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                context.Response.StatusCode = 500;
                context.Response.Close();
            }
        }
        public void Stop()
        {
            _httpListener.Stop();
            ConsoleExtension.WriteLine(
               $"HTTP-server successfully stopped at {_address}", ConsoleColor.Red);
        }
    }
}
