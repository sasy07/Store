using Application;
using Infrastructure;
using Web;
using Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

#region configuration

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.AddWebConfigureServices(builder.Configuration);

#endregion

#region build

var app = builder.Build();

await app.AddWebAppServices().ConfigureAwait(false);

#endregion