using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PontoPlus.Hubs;
using PontoPlus.Manager.Infra.Data;
using PontoPlus.Manager.Services.Services;
using System;

namespace PontoPlus
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
            var config = Configuration.GetConnectionString("DefaultConnection");

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<PontoPlusContext>(options =>
                options.UseMySql(config, builder =>
                builder.MigrationsAssembly("PontoPlus")));
            //options.UseInMemoryDatabase(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<SeendingService>();
            services.AddScoped<UsuarioServices>();
            services.AddScoped<UsuarioMensagemService>();
            services.AddScoped<RegistroPontoServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeendingService seendingService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            seendingService.Seed();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
