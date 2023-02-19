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
using SalesApp.Repository;
using SalesApp.Repository.Interface;
using Microsoft.EntityFrameworkCore.Design;
using System.Globalization;
using Microsoft.Extensions.Logging;
using System.IO;
using SalesApp.WebAPI.Service;
using SalesApp.WebAPI.Data.IData;
using SalesApp.WebAPI.Data;
using SalesApp.WebAPI.Service.IService;

namespace SalesApp
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
            services.AddAuthentication("CookieAuthentication")
                .AddCookie("CookieAuthentication", config =>
                {
                    config.Cookie.Name = "SalesCookie";
                    config.LoginPath = "/Account/Login";
                });
            services.AddCors(options =>
            {
                   options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    );
            });
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(1200);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews();
            services.AddDbContext<Sales_ERPContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("ERPConnection")));

            services.AddDbContext<ExportErpDbContext>(options =>
        options.UseSqlServer(
            Configuration.GetConnectionString("ExportERPSQLConnection")));

            services.AddScoped<IMirrorRepository, MirrorRepository>();
            services.AddScoped<ICashSaleRepository, CashSaleRepository>();
            services.AddScoped<ICustomSaleRepository, CustomSaleRepository>();
            services.AddScoped<IStandDeliveryRepository, StandDeliveryRepository>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<INormalSaleRepository, NormalSaleRepository>();
            services.AddScoped<IPrintRepository,PrintRepository>();
            services.AddScoped<IEditRepository, EditRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IUserRepository, UserRepository>();




            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductData, ProductData>();


            //services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddDirectoryBrowser();
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var cultureInfo = new CultureInfo("hi-IN");
            //  cultureInfo.NumberFormat.CurrencySymbol = "₹";

            //var path = Directory.GetCurrentDirectory();
            
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

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
           
            //  app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseRouting();
            app.UseCors("CorsPolicy");
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
