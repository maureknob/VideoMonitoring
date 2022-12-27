using VideoMonitoring.Data;
using VideoMonitoring.Services.ServerConnection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IServerConnection, ServerConnection>();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddControllers();  

var app = builder.Build();

app.MapControllers();

app.Run();
