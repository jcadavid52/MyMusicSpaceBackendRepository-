using Microsoft.Extensions.Options;
using MyMusicSpace.Api.ExceptionHandling;
using MyMusicSpace.Business;
using MyMusicSpace.Data;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Abstractions;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddBusinessServices();

builder.Services.AddHttpClient<IProviderMusicSearchService, SpotifySearchService>((sp,client) =>
{
    var providerMusicOptions = sp.GetRequiredService<IOptions<ProviderMusicOptions>>().Value;
    client.BaseAddress = new Uri(providerMusicOptions.Spotify.ApiUrl);
});

builder.Services.AddHttpClient<IProviderMusicGetAccessTokenService, SpotifyGetAccessTokenService>((sp,client) =>
{
    var providerMusicOptions = sp.GetRequiredService<IOptions<ProviderMusicOptions>>().Value;
    client.BaseAddress = new Uri(providerMusicOptions.Spotify.TokenUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
