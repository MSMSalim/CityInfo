using CityInfo.API.Context;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CityInfo.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc((Microsoft.AspNetCore.Mvc.MvcOptions options) =>
                {
                  options.EnableEndpointRouting = false;

                })
                .AddMvcOptions(o =>
                {
                    o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                })
                .AddNewtonsoftJson();

            //Register for DI
            //Transient for Lightweight stateless service
            // Scoped are created once for each request
            // Singleton first time it's requested and subsequent service uses that instance
            services.AddTransient<IMailService, LocalMailService>();
            services.AddScoped<ICityInfoRepository, CityInfoRepository>();

            // Register Dbcontext for entityframework
            // Below will register as scoped lifetime
            var connectionString = "Data Source=127.0.0.1,1433;Initial Catalog=CityInfoDB;User ID=sa;Password=P@55word";
            services.AddDbContext<CityInfoContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });
          }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }


            app.UseMvc();
            app.UseRouting();
            app.UseStatusCodePages();

            /*
                 app.UseEndpoints(endpoints =>
                 {
                     endpoints.MapGet("/", async context =>
                     {
                         await context.Response.WriteAsync("Hello World!");
                     });
                 });

             */
        }
    }
}
