namespace SweaterV1.Services.Middlewares;

public interface IUserProviderMiddleware
{
    int GetUserId();
    string GetUsername();
}