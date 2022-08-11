using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using SweaterV1.Infrastructure.Data;
using SweaterV1.Infrastructure.Repositories;
using SweaterV1.Services.Extensions;
using SweaterV1.Services.Services;
using SweaterV1.Services.Mapper;
using SweaterV1.Services.Options;

namespace SweaterV1.WebAPI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        services.AddControllersWithViews();
        services.AddAutoMapper(typeof(UserLoginProfile), typeof(UserRegistrationProfile),
            typeof(UserInformationProfile), typeof(UserChangeProfile),
            typeof(PostInformationProfile), typeof(PostChangeProfile), typeof(PostCreationProfile),
            typeof(CommentInformationProfile), typeof(CommentChangeProfile), typeof(CommentCreateProfile)
            , typeof(LikeInformationProfile), typeof(LikeCreateProfile));
        services.AddMvc();
        services.AddControllers();
        services.AddDbContext<SweaterDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddScoped<TokenMiddleware>();
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

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.ConfigureExceptionHandler(logger);
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}