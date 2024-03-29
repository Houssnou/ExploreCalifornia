﻿using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ExploreCalifornia.Filters
{
    internal class AutoAuthenticationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, 
            CancellationToken cancellationToken)
        {
            HttpContext.Current.User = new GenericPrincipal(
                new GenericIdentity("username"),
                new string[] { });

            return base.SendAsync(request, cancellationToken);
        }
    }
}