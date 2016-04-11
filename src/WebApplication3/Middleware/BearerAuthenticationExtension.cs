using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Middleware
{
    public static class BearerAuthenticationExtension
    {
        public static IApplicationBuilder UseBearerAuthentication(this IApplicationBuilder @this,Action<BearerOption> config)
        {
            var options = new BearerOption();
            if (config != null)
                config(options);
            return @this.UseBearerAuthentication(options);
        }

        public static IApplicationBuilder UseBearerAuthentication(this IApplicationBuilder @this, BearerOption options)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return @this.UseMiddleware<BearerAuthenticationMiddleware>(options);
        }
    }
}
