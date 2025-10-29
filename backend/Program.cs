using RealEstateAPI.Services;
using RealEstateAPI.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure MongoDB settings
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB"));

// Register PropertyService
builder.Services.AddSingleton<PropertyService>();

// Add controllers
builder.Services.AddControllers();

// Add Swagger (for .NET 8)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Real Estate API",
        Version = "v1",
        Description = "API for managing property data from MongoDB"
    });
});

var app = builder.Build();

// Enable Swagger UI only in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Real Estate API v1");
        c.RoutePrefix = string.Empty; // makes Swagger UI the root
    });
}

app.MapControllers();
app.Run();
