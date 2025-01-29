using HospitalSystemBlazor.Data;
using HospitalSystemBlazor.Entities.DTOs;
using HospitalSystemBlazor.Service;
using HospitalSystemBlazor.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IService<EspecialidadDTO>, EspecialidadService>();
builder.Services.AddScoped<IService<UsuarioDto>, UsuarioService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexionSQL"));
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:7025")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MiClaveDeSeguridadEsLaMejorYmasSeguraDelMundoPorFavorEvitarUsarla")),
            ValidateIssuer = true,
            ValidIssuer = "SUPERKEYCLAVESSS",
            ValidateAudience = true,
            ValidAudience = "SUPERKEYCLAVESSS",
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            RoleClaimType = ClaimTypes.Role
        };

        
    });


builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
    opt.AddPolicy("Trabajador", policy => policy.RequireRole("Trabajador"));
    opt.AddPolicy("Paciente", policy => policy.RequireRole("Paciente"));
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();


app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
