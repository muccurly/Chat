using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ManualHttpServer.Extensions;
using Soilesu.Server.Core.Attributes;
using Soilesu.Server.Core.Interfaces;
using Soilesu.Server.Core.Models;
using Soilesu.Server.Resources.Pages;

namespace Soilesu.Server.Core
{
    class RoutingProvider
    {
       private Dictionary<string, Type> _routes;
        public RoutingProvider()
        {
            _routes = DiscoverRoutes();
            foreach (var route in _routes)
                PrintRoute(route.Key, route.Value.Name);
        }       
        private void PrintRoute(string path, string name)
        {
            ConsoleExtension.WriteLine($"Discovered rote: {name} | {path}",
                ConsoleColor.Yellow);
        }
        public IResourceProvider GetResourceProviderByRoute(string route)
        {
            if (_routes.ContainsKey(route))
            {
                var provider = Activator.CreateInstance(_routes[route]);
                return provider as IResourceProvider;
            }
            else
            {
                throw new InvalidOperationException("Route not found!");
            }
        }
        public void SetResourceProviderByRoute(string route)
        {
            if (!_routes.ContainsKey(route))
            {
                _routes.Add(route, typeof(ConfirmEmailPageResource));               
            }
           
        }
        public void RemoveResourceProviderByRoute(string route)
        {
            if (_routes.ContainsKey(route))
                _routes.Remove(route);
        }
        private Dictionary<string, Type> DiscoverRoutes()
        {
            var resources = Assembly
                 .GetExecutingAssembly()
                 .GetTypes()
                 .Where(p => typeof(IResourceProvider).IsAssignableFrom(p) &&
                     !p.IsAbstract && !p.IsInterface &&
                     p.GetCustomAttribute(typeof(ResourceRouteAttribute)) != null);

            var routeToResourceMap = resources
               .ToDictionary(p =>
                   GetAbsolutePathToResource(p.GetCustomAttribute<ResourceRouteAttribute>()));
            using (var db = new ChatContext())
            {
               List<User>users = db.Users.ToList();
                foreach (var user in users)
                {
                   if(user.Verified == false)
                routeToResourceMap.Add("/api/activation/"+user.Token,typeof(ConfirmEmailPageResource));
                }
            }
            return routeToResourceMap;
        }

        private string GetAbsolutePathToResource(ResourceRouteAttribute resourceRouteAttribute)
        {
            var path = $"/{resourceRouteAttribute.Route}";
            return path;
        }
    }
}
