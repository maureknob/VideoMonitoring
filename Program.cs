using VideoMonitoring.Data;
using VideoMonitoring.Repositories;
using VideoMonitoring.Services.ServerServices;
using VideoMonitoring.Services.VideoServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IServerRepository, ServerRepository>();
builder.Services.AddScoped<IServerConnection, ServerConnection>();
builder.Services.AddScoped<IVideoFileHandler, VideoFileHandler>();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddControllers();  

var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapControllers();

app.Run();
