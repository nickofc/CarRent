using System.Linq;
using CarRental.Infrastructure.Database;
using CarRental.Infrastructure.Extensions;
using CarRental.Infrastructure.Options;
using CarRental.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CarRental.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddOptions();
            services.Configure<DatabaseOptions>(Configuration.GetSection("Database"));
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IRentService, RentService>();

            services.AddDbContext<Context>(options =>
            {
                var databaseOptions = services.Resolve<IOptions<DatabaseOptions>>().Value;
                if (databaseOptions.InMemory)
                    options.UseInMemoryDatabase("dbName");
                else
                    options.UseSqlite(databaseOptions.ConnectionString);
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            var options = app.Resolve<IOptions<DatabaseOptions>>().Value;
            if (options.Seed)
            {
                var context = app.Resolve<Context>();
                if (options.ForceSeed)
                {
                    context.Seed();
                }
                else
                {
                    if (!(context.Vehicles.Any() || context.Rentals.Any())) // if db is empty
                    {
                        context.Seed();
                    }
                }
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Vehicle}/{action=Vehicles}/{id?}");
            });
        }
    }
}
