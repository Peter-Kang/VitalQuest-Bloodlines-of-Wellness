using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.Extensions.DependencyInjection;
using VitaQuest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();//Blazor app, can mix and match with razor page or even mvc
builder.Services.AddSingleton<WeatherForecastService>();


DataBaseSetupService service = new DataBaseSetupService();
service.Setup();

builder.Services.AddTransient<CSVDataService>();

//https://github.com/dotnet/aspnetcore/issues/38212
{
    StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();//Map blazor SignalR Hub server to client.
app.MapFallbackToPage("/_Host");

app.Run();
