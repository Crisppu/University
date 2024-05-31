//1- usos para trabajar con EntidadFranwork
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniversityApiBackend;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Services;

var builder = WebApplication.CreateBuilder(args);//codigo generado del proyecto
//2- conexion con Sql server
const string CONNECTIONNAME = "UniversityDB1";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

//3- add contexto a los servicios de buider
//builder.Services.AddDbContext<UniversityDBContext1>(option => option.UseSqlServer(connectionString));
builder.Services.AddDbContext<UniversityDBContext>(option => option.UseSqlServer(connectionString));
//aqui va el 7 - Add Service of JWT Autorization
builder.Services.AddJwtTokenServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
//4. Add Custom Services (folder Services) - Agregar servicios personalizados (carpeta Servicios)
builder.Services.AddScoped<IStudentsServices, StudensService>();
/*TODO: Add the rest of services
aqui todos los controles como por ejemplo Studen
 */
//9. Add  Authorization - para que Swagger muestre informacion luego de que sea autorizado
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("userOnly", "User1"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//8. todo: configurar Swagger para encargarse de la autorización de JWT
//muestra el boton en Swagger authorize
builder.Services.AddSwaggerGen(options =>
//con esta cnfiguracion swagger solicite un token cuando estemos solicitando entrar a la rutas protejidas
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using Bearer Scheme"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]
            {
            }
        }
    });
});
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
