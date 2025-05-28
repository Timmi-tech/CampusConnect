using ChatSystem_1.Extensions;
using ChatSystem_1.Domain;
using Serilog;
using ChatSystem_1.Application.Mappings;
using ChatSystem_1.Api.ActionFilters;
using DotNetEnv;


Env.Load();


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Host.ConfigureSerilogService(); 
builder.Services.ConfigureLoggerService();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.ConfigurePostGressContext(builder.Configuration);
builder.Services.ConfigureServiceManager();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddControllers();
builder.Services.AddCloudinaryConfiguration(builder.Configuration);
builder.Services.ConfigurePhotoService();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
var logger = app.Services.GetRequiredService<ILoggerManager>(); 
app.ConfigureExceptionHandler(logger); 

if (app.Environment.IsProduction())
{
    app.UseHsts();
}


app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Campus Connect v1");
}); 

app.UseHttpsRedirection();


app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication();

app.MapControllers();

app.Run();

