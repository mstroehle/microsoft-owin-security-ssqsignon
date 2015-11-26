
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Microsoft.Owin.Security.SSQSignon
{
    public class SSQSignonAthenticationFilter : HostAuthenticationFilter
    {
        public SSQSignonAthenticationFilter()
            :base(SSQSignonDefaults.AuthenticationType)
        {
        }
    }
}
