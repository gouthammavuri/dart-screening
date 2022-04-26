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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

app.UseEndpoints(x => x.MapControllers());
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});

app.Run();
