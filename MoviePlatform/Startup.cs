using Microsoft.EntityFrameworkCore;
using MoviePlatform.Models;
using MoviePlatform.Service;

namespace MoviePlatform
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
            services.AddControllersWithViews();

            services.AddDbContext<MoviePlatformDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MoviePlatformContext")));

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Movie}/{action=GetMovies}/{id?}");
            });
        }
    }
}
