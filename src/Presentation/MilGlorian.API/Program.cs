using Microsoft.EntityFrameworkCore;
using MilGlorian.Persistence;
using MilGlorian.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.ConfigurePersistenceServices();
builder.Services.AddDbContext<MilGlorianDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration["ConnectionStrings:PostgreSQL"]);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
