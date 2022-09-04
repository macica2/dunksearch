using DunkSearch.Domain;
using DunkSearch.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DunkSearch.WebApp
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
            services.AddDbContext<DataContext>(p => p.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<ICaptionService, CaptionService>();
            services.AddTransient<IChannelService, ChannelService>();
            services.AddTransient<IVideoService, VideoService>();
            services.AddTransient<ICaptionTypeService, CaptionTypeService>();
            services.AddTransient<IUnsupportedVideoService, UnsupportedVideoService>();
            services.AddTransient<IAppEventLogService, AppEventLogService>();
            var mvcBuilder = services.AddControllersWithViews();
#if DEBUG
            mvcBuilder.AddRazorRuntimeCompilation(); // makes it so we can refresh browser to see cshtml changes during development
#endif
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

            app.UseRouting();

            // Supports ability to grab remote IP address of user for logging purposes
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                ForwardedHeaders.XForwardedProto
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
