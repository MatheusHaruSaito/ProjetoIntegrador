using System;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using unolink.api.Application.Services.OngTicketService;
using unolink.api.Application.Services.AuthService;
using unolink.api.Application.Services.UserRoleService;
using unolink.api.Application.Services.UserService;
using unolink.domain.Core.Interfaces;
using unolink.domain.Models;
using unolink.infrastructure.Context;
using unolink.infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IOngTicketRepository, OngTicketRepository>();
builder.Services.AddScoped<IOngTicketService, OngTicketService>();
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin();
    });
});

var app = builder.Build();
app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapSwagger();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
