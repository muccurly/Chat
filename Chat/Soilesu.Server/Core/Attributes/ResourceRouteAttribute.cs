using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soilesu.Server.Core.Attributes
{
   public class ResourceRouteAttribute:Attribute
    {
      public string Route { get;private set; }
      public ResourceRouteAttribute(string route)
        {
            Route = route;
        }
    }
}
