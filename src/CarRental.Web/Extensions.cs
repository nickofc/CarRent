using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Web
{
    public static class Extensions
    {
        public static T Resolve<T>(this IApplicationBuilder app)
        {
            return app.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<T>();
        }
    }
}
