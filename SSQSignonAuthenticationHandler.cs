using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.SSQSignon
{
    
    public class SSQSignonAuthenticationHandler : AuthenticationHandler<SSQSignonAuthenticationOptions>
    {
        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var authHeaderKV = Request.Headers.SingleOrDefault(h => h.Key.Equals("authorization", StringComparison.InvariantCultureIgnoreCase));
            if (!authHeaderKV.Equals(default(KeyValuePair<string, string[]>)) && authHeaderKV.Value.Count() == 1)
            {
                var authHeader = authHeaderKV.Value.First();
                var response = RestClient.Execute<WhoAmIResponse>(WhoAmIRequest.AddHeader("authorization", authHeader));
                if (response.ErrorException == null && response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    return Task.FromResult<AuthenticationTicket>(BuildAuthenticationTicket(response.Data));
                }
            }
            return Task.FromResult<AuthenticationTicket>(new AuthenticationTicket(null, null));
        }

        private class WhoAmIResponse
        {
            public string Scope { get; set; }

            public string UserId { get; set; }

            public string ExpiresOn { get; set; }
        }

        private RestSharp.RestClient RestClient
        {
            get { return new RestSharp.RestClient("https://ssqsignon.com"); }
        }

        private RestSharp.RestRequest WhoAmIRequest
        {
            get { return new RestSharp.RestRequest(string.Format("/{0}/whoami", Options.Module)); }
        }

        private DateTime Now
        {
            get { return DateTime.UtcNow; }
        }

        private AuthenticationTicket BuildAuthenticationTicket(WhoAmIResponse response)
        {
            var identity = new ClaimsIdentity(Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, response.UserId, null, Options.AuthenticationType));
            identity.AddClaim(new Claim(ClaimTypes.Name, response.UserId, null, Options.AuthenticationType));
            response.Scope.Split(' ').ToList().ForEach(r => identity.AddClaim(new Claim(ClaimTypes.Role, r, System.Security.Claims.ClaimValueTypes.String, Options.AuthenticationType)));

            var properties = new AuthenticationProperties();

            return new AuthenticationTicket(identity, properties);
       }
    }
}
