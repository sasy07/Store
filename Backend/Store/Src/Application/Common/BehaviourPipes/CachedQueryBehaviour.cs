using System.Text;
using Application.Contracts;
using Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Application.Common.BehaviourPipes;

public class CachedQueryBehaviour<TRequest, TResponse>(IDistributedCache cache ,IHttpContextAccessor httpContextAccessor) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICacheQuery, IRequest<TResponse>
{
    private readonly IDistributedCache _cache = cache;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response;
        var key = GenerateKey();
        var cachedResponse = await _cache.GetAsync(key, cancellationToken);
        if (cachedResponse!=null)
        {
            response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse));
        }
        else
        {
            response = await next();
            var serialized = Encoding.Default.GetBytes(JsonConvert.SerializeObject(response));
            await CreateNewCache(request,key, cancellationToken, serialized);
        }

        return response;
    }

    private async Task CreateNewCache(TRequest request,string key, CancellationToken cancellationToken, byte[] serialized)
        => _cache.SetAsync(key, serialized, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeToLive(request)
        }, cancellationToken);

    private static TimeSpan? TimeToLive(TRequest request)
        => new TimeSpan(request.HoursSaveData, 0,  0);
    private string GenerateKey()
        => IdGenerator.GenerateCacheKeyFromRequest(_httpContextAccessor.HttpContext.Request);
}