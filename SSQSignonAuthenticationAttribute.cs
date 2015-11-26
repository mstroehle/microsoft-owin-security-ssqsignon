
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Microsoft.Owin.Security.SSQSignon
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class SSQSignonAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        private readonly IAuthenticationFilter _innerFilter;

        public SSQSignonAuthenticationAttribute()
            : this(new SSQSignonAthenticationFilter())
        {
        }

        internal SSQSignonAuthenticationAttribute(IAuthenticationFilter innerFilter)
        {
            if (innerFilter == null)
            {
                throw new ArgumentNullException("innerFilter");
            }

            _innerFilter = innerFilter;
        }

        public bool AllowMultiple
        {
            get { return true; }
        }

        public string AuthenticationType
        {
            get { return SSQSignonDefaults.AuthenticationType; }
        }

        internal IAuthenticationFilter InnerFilter
        {
            get
            {
                return _innerFilter;
            }
        }

        /// <inheritdoc />
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            return _innerFilter.AuthenticateAsync(context, cancellationToken);
        }

        /// <inheritdoc />
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return _innerFilter.ChallengeAsync(context, cancellationToken);
        }
    }
}
