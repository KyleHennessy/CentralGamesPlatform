﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CentralGamesPlatform.Models;
using Stripe;
using Microsoft.AspNetCore.Identity;
using CentralGamesPlatform.Repositories;
using CentralGamesPlatform.IRepositories;

namespace CentralGamesPlatform
{
    public class Startup
    { 
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores <MyDatabaseContext>();
            services.AddControllersWithViews();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<ILicenceRepository, LicenceRepository>();
            services.AddScoped<ICasinoPassRepository, CasinoPassRepository>();
            services.AddScoped<IResultRepository, ResultRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IPayoutRepository, PayoutRepository>();
            services.AddScoped<IVerificationRepository, VerificationRepository>();
            services.AddScoped<IDownloadRepository, DownloadRepository>();
            services.AddScoped<ShoppingCart>(sc => ShoppingCart.GetCart(sc));
            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddRazorPages();
            services.AddDbContext<MyDatabaseContext>(options =>
                   // options.UseSqlite("Data Source=localdatabase.db"));
                   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.ApiKey = (Configuration.GetSection("Stripe")["TestSecretKey"]);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            //StripeConfiguration.SetApiKey(Configuration.GetConnectionString("StripeTestSecretKey"));
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseStatusCodePages();
            //app.UseStatusCodePagesWithReExecute("/Error/HandleError/{0}");
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Error/HandleError";
                    await next();
                }
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"areas",
                    pattern:"{area:exists}/{controller=Play}/{action=Index}/{id?}"
                    );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapRazorPages();
            });
        }
    }
}
