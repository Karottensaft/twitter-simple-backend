using Microsoft.EntityFrameworkCore;
using SweaterV1.Infrastructure.Data;
using SweaterV1.Infrastructure.Repositories;
using SweaterV1.Services.Services;
using SweaterV1.Services.Mapper;

namespace SweaterV1.WebAPI;

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
        services.AddAutoMapper(typeof(UserLoginProfile), typeof(UserRegistrationProfile), typeof(UserInformationProfile), typeof(UserChangeProfile),
            typeof(PostInformationProfile), typeof(PostChangeProfile), typeof(PostCreationProfile));
        services.AddMvc();
        services.AddControllers();
        services.AddDbContext<SweaterDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<UserRepository>();
        services.AddScoped<PostRepository>();
        services.AddScoped<UnitOfWork>();
        services.AddScoped<UserService>();
        services.AddScoped<PostService>();
        services.AddScoped<PostRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}