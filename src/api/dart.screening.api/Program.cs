using dart.screening.core;
using dart.screening.core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Registering services
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddCors();

builder.Services.Configure<MarsRoverOptions>(
    builder.Configuration.GetSection(MarsRoverOptions.MarsRoverSettings));

builder.Services.AddScoped<IMarsRoverService, MarsRoverService>();

var app = builder.Build();

app.UseRouting();
app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

app.UseEndpoints(x => x.MapControllers());
app.Run();
