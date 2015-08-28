using Microsoft.Owin.Security.SSQSignon;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owin
{
    public static class SSQSignonAuthenticationExtensions
    {
        public static IAppBuilder UseSSQSignonAuthentication(this IAppBuilder app, string module)
        {
            return app.Use(typeof(SSQSignonAuthenticationMiddleware), app, new SSQSignonAuthenticationOptions(module));
        }

        public static IAppBuilder UseSSQSignonAuthentication(this IAppBuilder app, SSQSignonAuthenticationOptions options)
        {
            return app.Use(typeof(SSQSignonAuthenticationMiddleware), app, options);
        }
    }
}
