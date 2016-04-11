using Microsoft.AspNet.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Middleware
{
    public class BearerAuthenticationHandler : AuthenticationHandler<BearerOption>
    {
        public BearerAuthenticationHandler()
        {
            
        }
        public override Task<bool> HandleRequestAsync()
        {
            return base.HandleRequestAsync();
        }

        #pragma warning disable 1998
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return AuthenticateResult.Failed(new Exception("unknow operate"));
            
        }
    }
}
