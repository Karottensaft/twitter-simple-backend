using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SweaterV1.Domain.Models;
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
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // укзывает, будет ли валидироваться издатель при валидации токена
                    ValidateIssuer = true,
                    // строка, представляющая издателя
                    ValidIssuer = AuthOptions.ISSUER,

                    // будет ли валидироваться потребитель токена
                    ValidateAudience = true,
                    // установка потребителя токена
                    ValidAudience = AuthOptions.AUDIENCE,
                    // будет ли валидироваться время существования
                    ValidateLifetime = true,

                    // установка ключа безопасности
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    // валидация ключа безопасности
                    ValidateIssuerSigningKey = true,
                };
            });
        services.AddControllersWithViews();
        services.AddAutoMapper(typeof(UserLoginProfile), typeof(UserRegistrationProfile), typeof(UserInformationProfile), typeof(UserChangeProfile),
            typeof(PostInformationProfile), typeof(PostChangeProfile), typeof(PostCreationProfile),
            typeof(CommentInformationProfile), typeof(CommentChangeProfile), typeof(CommentCreateProfile)
            , typeof(LikeInformationProfile), typeof(LikeCreateProfile));
        services.AddMvc();
        services.AddControllers();
        services.AddDbContext<SweaterDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<UserRepository>();
        services.AddScoped<PostRepository>();
        services.AddScoped<CommentRepository>();
        services.AddScoped<LikeRepository>();
        services.AddScoped<UnitOfWork>();
        services.AddScoped<UserService>();
        services.AddScoped<PostService>();
        services.AddScoped<CommentService>();
        services.AddScoped<LikeService>();
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
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); 

        });
    }
}