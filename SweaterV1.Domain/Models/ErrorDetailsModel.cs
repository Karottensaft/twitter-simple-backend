using System.Text.Json;

namespace SweaterV1.Domain.Models;

public class ErrorDetailsModel
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}