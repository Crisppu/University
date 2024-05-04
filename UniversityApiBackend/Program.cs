//1- usos para trabajar con EntidadFranwork
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Services;

var builder = WebApplication.CreateBuilder(args);//codigo generado del proyecto
//2- conexion con Sql server
const string CONNECTIONNAME = "UniversityDB1";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

//3- add contexto a los servicios de buider
//builder.Services.AddDbContext<UniversityDBContext1>(option => option.UseSqlServer(connectionString));
builder.Services.AddDbContext<UniversityDBContext>(option => option.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
//4. Add Custom Services (folder Services) - Agregar servicios personalizados (carpeta Servicios)
builder.Services.AddScoped<IStudentsServices, StudensService>();
/*TODO: Add the rest of services
aqui todos los controles como por ejemplo Studen
 */
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//5. habilitar el CORS, configurar
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin(); //cualquier aplicacion tendra acceso a hacer peticiones
        builder.AllowAnyMethod();//acceso a los get,post,get,update cualquier peticion
        builder.AllowAnyHeader();//cualquier cabezera
    });
});

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
//6 decirle a la aplicación que use CORS
app.UseCors("CorsPolicy");

app.Run();
