using Microsoft.EntityFrameworkCore;
using TaskManagement.API.MappingProfiles;
using TaskManagement.API.ServicesExtensions;
using TaskManagement.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.WorkUnit();
builder.Services.AddRepository();
builder.Services.AddBusiness();
builder.Services.AddServices();
builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.ConfigureSwagger();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
    context.Database.Migrate();

    await context.SaveChangesAsync();   
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
