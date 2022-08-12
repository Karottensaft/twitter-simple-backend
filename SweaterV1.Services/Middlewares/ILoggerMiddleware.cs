namespace SweaterV1.Services.Middlewares;

public interface ILoggerMiddleware
{
    void LogInfo(string message);
    void LogWarn(string message);
    void LogDebug(string message);
    void LogError(string message);
}