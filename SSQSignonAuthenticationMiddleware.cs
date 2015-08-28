using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.SSQSignon
{
    public class SSQSignonAuthenticationMiddleware : AuthenticationMiddleware<SSQSignonAuthenticationOptions>
    {

        public SSQSignonAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app, SSQSignonAuthenticationOptions options)
            : base(next, options)
        {
        }

        // Called for each request, to create a handler for each request.
        protected override AuthenticationHandler<SSQSignonAuthenticationOptions> CreateHandler()
        {
            return new SSQSignonAuthenticationHandler();
        }
    }
}
