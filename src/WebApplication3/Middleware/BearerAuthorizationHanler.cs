using Microsoft.AspNet.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Middleware
{
    public class BearerAuthorizationHanler : AuthorizationHandler<BearerAuthorizationRequirement>
    {
        protected override void Handle(AuthorizationContext context, BearerAuthorizationRequirement requirement)
        {
            throw new NotImplementedException();
        }
    }
}
