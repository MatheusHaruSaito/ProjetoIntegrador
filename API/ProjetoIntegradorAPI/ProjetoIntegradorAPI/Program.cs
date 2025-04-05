using Microsoft.EntityFrameworkCore;
using ProjetoIntegradorAPI.Context;
using ProjetoIntegradorAPI.Models;
using ProjetoIntegradorAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddCors();
builder.Services.AddDbContext<ApplicationDataContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(o =>
{
    o.AllowAnyHeader();
    o.AllowAnyMethod();
    o.AllowAnyOrigin();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
