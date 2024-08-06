using System.Text;
using Microsoft.AspNetCore.Http;

namespace Application.Helpers;

public class IdGenerator
{
    public static string GenerateCacheKeyFromRequest(HttpRequest request)
    {
        var keyBuilder = new StringBuilder();
        keyBuilder.Append($"{request.Path}");
        foreach (var (key , value) in request.Query.OrderBy(x=>x.Key))
        {
            keyBuilder.Append($"|{key}-{value}");
        }

        return keyBuilder.ToString();
    }
}