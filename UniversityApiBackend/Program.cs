//1- usos para trabajar con EntidadFranwork
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;

var builder = WebApplication.CreateBuilder(args);//codigo generado del proyecto
//2- conexion con Sql server
const string CONNECTIONNAME = "UniversityDB1";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

//3- add contexto a los servicios de buider
//builder.Services.AddDbContext<UniversityDBContext1>(option => option.UseSqlServer(connectionString));
builder.Services.AddDbContext<UniversityDBContext>(option => option.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
