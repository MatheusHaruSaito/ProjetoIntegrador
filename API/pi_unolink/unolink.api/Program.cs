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
using Microsoft.AspNetCore.Identity;
using unolink.api.Application.Services.ImagesSevice;
using unolink.api.Application.Services.UserPostService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//DefaultConnection
//FatecConnection
builder.Services.AddDbContext<ApplicationDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FatecConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IOngTicketRepository, OngTicketRepository>();
builder.Services.AddScoped<IOngTicketService, OngTicketService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFilesService, FilesService>();
builder.Services.AddScoped<IUserPostRepository, UserPostRepository>();
builder.Services.AddScoped<IUserPostService, UserPostService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDataContext>()
    .AddDefaultTokenProviders();

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


app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    var roles = new[] { "Admin","Ong","Default" };
    
    foreach(var role in roles)
    {
        if(! await roleManager.RoleExistsAsync(role))
        {
           await roleManager.CreateAsync(new IdentityRole<Guid>(role));
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    string email = "SuperOngAdmin@Admin.com";
    string password = "Admin123@AD";
    if (await userManager.FindByEmailAsync(email) is null)
    {
        var user = new User();
        user.Email = email;
        user.UserName = email;

        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Admin");
    }
}

    app.Run();
