using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MilGlorian.Persistence;
using MilGlorian.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.ConfigurePersistenceServices();
builder.Services.AddDbContext<MilGlorianDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration["ConnectionStrings:PostgreSQL"]);
});

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddOptionsWithValidateOnStart<Program>();//ensure
builder.Services.AddSwaggerDocument(configure =>
{
    configure.PostProcess = (doc =>
    {
        doc.Info.Title = "Mil Glorian Jobs";
        doc.Info.Version = "1.0";
        doc.Info.Description = "All Jobs in Azerbaijan";
        doc.Info.Contact = new NSwag.OpenApiContact()
        {
            Name = "MIL GLORIAN",
            Url = "https://www.youtube.com/@iamjabiev",
            Email = "jabieviam@gmail.com",
        };
    });
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

app.MapGet("/", handler =>
{
    handler.Response.Redirect("/swagger/index.html", permanent: false);
    return Task.CompletedTask;
});

app.UseAuthorization();

app.MapControllers();

app.Run();
