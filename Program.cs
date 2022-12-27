using VideoMonitoring.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
