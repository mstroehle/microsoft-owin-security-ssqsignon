using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Owin.Security.SSQSignon
{
    public class SSQSignonAuthenticationOptions : AuthenticationOptions
    {
        public SSQSignonAuthenticationOptions(string module)
            :base(SSQSignonDefaults.AuthenticationType)
        {
            Module = module;
            Description.Caption = AuthenticationType;
            AuthenticationMode = AuthenticationMode.Passive;
        }

        public string Module { get; set; }
    }
}
