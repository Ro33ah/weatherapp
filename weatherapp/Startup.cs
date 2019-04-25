
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using weatherapp.Services;
using weatherapp.Models;
using Microsoft.EntityFrameworkCore;
using weatherapp.Repository;

namespace weatherapp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHttpClient<IGetWeather, GetWeather>();
            services.AddDbContext<ApplicationContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:HistoryDB"]));
            services.AddTransient<ISearchHistoryRepository<WeatherModel>, SearchHistoryManager>();
            services.AddSingleton<IWeatherService, WeatherService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(builder =>
            builder.WithOrigins("http://localhost:8080").AllowAnyHeader());
            app.UseCors(builder =>
            builder.WithOrigins("http://192.168.0.102:8080").AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
