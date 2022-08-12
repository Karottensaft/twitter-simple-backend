using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;

namespace SweaterV1.Services.Middlewares;

public static class ExceptionLogsMiddleware
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerMiddleware logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null) logger.LogError($"Something went wrong: {contextFeature.Error}");
                return null!;
            });
        });
    }
}