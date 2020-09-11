using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Soilesu.Server.Core.Interfaces
{
    public interface IResourceProvider
    {
        void Process(HttpListenerRequest request, HttpListenerResponse response);
    }
}
