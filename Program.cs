global using elemental_heroes_server.Models;
global using elemental_heroes_server.Auth;
global using elemental_heroes_server.Models.Enumerable;
global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using System.Security.Claims;
global using elemental_heroes_server.DTOs.WeaponDtos;
global using elemental_heroes_server.DTOs.GameDtos;
using elemental_heroes_server.Data;
using elemental_heroes_server.Services.HeroService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using elemental_heroes_server.Services.SkillService;
using elemental_heroes_server.Services.WeaponService;
using elemental_heroes_server.Services.OpenChestService;
using elemental_heroes_server.Services.GameService;

// Enable CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add the DbContext
builder.Services.AddDbContext<DataContext>(options
    => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreElephantSQL")));

// Service DI
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IHeroService, HeroService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IWeaponService, WeaponService>();
builder.Services.AddScoped<IOpenChestService, OpenChestService>();
builder.Services.AddScoped<IGameService, GameService>();

// Add JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            builder.Configuration.GetSection("AppSettings:Token").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
