using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportStore.Models;
using Microsoft.AspNetCore.Identity;

namespace SportStore
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Adding MVC
            services.AddControllersWithViews();
            services.AddDbContext<StoreDbContext>(o => o.UseSqlServer(Configuration["ConnectionStrings:SportStoreConnection"]));
            services.AddScoped<IStoreRepository, EFStoreRepository>();
            services.AddScoped<IOrderRepository, EFOrderRepository>();
            services.AddRazorPages();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddScoped<Cart>(x => SessionCart.GetCart(x));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddServerSideBlazor();

            services.AddDbContext<AppIdentityDbContext>(o => o.UseSqlServer(Configuration["ConnectionStrings:IdentityConnection"]));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("catpage",
                    "{category}/Page{productPage:int}",
                     new { Controller = "Home", action = "Index" });

                endpoints.MapControllerRoute("page",
                    "Page{productPage:int}",
                     new { Controller = "Home", action = "Index", productPage = 1 });

                endpoints.MapControllerRoute("category",
                    "{category}",
                    new { Controller = "Home", action = "Index", productPage = 1 });

                endpoints.MapControllerRoute("pagination",
                    "Prodcts/Page{productPage}",
                    new { Controller = "Home", action = "Index", productPage = 1 });

                endpoints.MapControllerRoute("delivery",
                    "Delivery",
                    new { Controller = "Home", action = "Delivery"});

                endpoints.MapControllerRoute("faq",
                    "FAQ",
                    new { Controller = "Home", action = "FAQ" });

                endpoints.MapControllerRoute("about",
                    "About",
                    new { Controller = "Home", action = "About" });

                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}", "/Index");
            });

            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
