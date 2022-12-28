using Microsoft.OpenApi.Models;
using VideoMonitoring.Data;
using VideoMonitoring.Repositories;
using VideoMonitoring.Services.ServerServices;
using VideoMonitoring.Services.VideoServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IServerRepository, ServerRepository>();
builder.Services.AddScoped<IServerConnection, ServerConnection>();
builder.Services.AddScoped<IVideoFileHandler, VideoFileHandler>();
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VideoMonitoring", Version = "v1.1" });
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
