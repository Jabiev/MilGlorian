using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence;
using MilGlorian.Persistence.Contexts;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.ConfigurePersistenceServices();

#region JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"]))
    };
});

builder.Services.AddAuthorization();
#endregion

#region Db & DbInitial
builder.Services.AddDbContext<MilGlorianDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration["ConnectionStrings:PostgreSQL"]);
});

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<MilGlorianDbContext>();//DON'T FORGET

builder.Services.AddScoped<MilGlorianDbContextInitializer>();
#endregion


builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddOptionsWithValidateOnStart<Program>();//ensure

#region Swagger Doc
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
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var instance = scope.ServiceProvider.GetRequiredService<MilGlorianDbContextInitializer>();
    await instance.InitializeAsync();
    await instance.RoleSeedAsync();
    await instance.UserSeedAsync();
}

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
