using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SALEERP.Data;
using SALEERP.Repository.Interface;
using SALEERP.Repository;

namespace SALEERP
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
          
            services.AddSession(so =>
            {
                so.IdleTimeout = TimeSpan.FromSeconds(60);
            });
            services.AddAuthentication("CookieAuthentication")
                 .AddCookie("CookieAuthentication", config =>
                 {
                     config.Cookie.Name = "UserLoginCookie";
                     config.LoginPath = "/Account/Login";
                 });
            services.AddControllersWithViews();
            services.AddDbContext<Sales_ERPContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("ERPConnection")));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPoolRepository, PoolRepository>();
            services.AddScoped<IAgentRepository,AgentRepository>();
            services.AddScoped<ISeriesRepository, SeriesRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IAgentUserRepository, AgentUserRepository>();
            services.AddScoped<IMirrorRepository, MirrorRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>();
            services.AddScoped<IcommissionRepository, CommissionRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // who are you?  
            app.UseAuthentication();

            // are you allowed?  
           
            app.UseRouting();
            
            app.UseSession();
            // app.UseAuthorization();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Index}/{id?}");
            });
        }
    }
}
