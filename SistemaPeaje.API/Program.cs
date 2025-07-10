using Microsoft.EntityFrameworkCore;
using SistemaPeaje.API.Data;
using SistemaPeaje.API.Repositories;
using SistemaPeaje.API.Services;
using SistemaPeaje.API.Helpers;
using SistemaPeaje.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Entity Framework
builder.Services.AddDbContext<TollSystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<ITollTransactionRepository, TollTransactionRepository>();

// Add services
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<ITollService, TollService>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Add API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "Sistema de Peaje API", 
        Version = "v1",
        Description = "API para el manejo de un sistema de peaje"
    });
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema de Peaje API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at app's root
    });
}

// Add exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TollSystemContext>();
    await context.Database.EnsureCreatedAsync();
}

app.Run();
