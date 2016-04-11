using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Middleware
{
    public class BearerFilter : AuthorizeFilter
    {
        public BearerFilter(AuthorizationPolicy policy) : base(policy)
        {
            
        }
        

        public override async Task OnAuthorizationAsync(Microsoft.AspNet.Mvc.Filters.AuthorizationContext context)
        {
            if (!context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                if (!context.Filters.Any(item => item is IAsyncAuthorizationFilter&&item.GetType()!=this.GetType()))
                {
                    await base.OnAuthorizationAsync(context);
                }

                //to do:权限判断

            }
        }
    }
}
