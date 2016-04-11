using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WebEncoders;
using Microsoft.Extensions.OptionsModel;

namespace WebApplication3.Middleware
{
    public class BearerAuthenticationMiddleware: AuthenticationMiddleware<BearerOption>
    {
        public BearerAuthenticationMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory,
            IUrlEncoder encoder,
            BearerOption options)
            : base(next, options, loggerFactory, encoder)
        {

        }

        protected override AuthenticationHandler<BearerOption> CreateHandler()
        {
            return new BearerAuthenticationHandler();
        }

        
    }
}
