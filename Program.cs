using VideoMonitoring.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddControllers();  

var app = builder.Build();

app.MapControllers();

app.Run();
