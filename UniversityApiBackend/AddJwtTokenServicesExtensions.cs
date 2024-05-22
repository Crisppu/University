using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend
{
    public static class AddJwtTokenServicesExtensions
    {
        public static void AddJwtTokenServices(this IServiceCollection Services, IConfiguration Configuration) {
            //Add JWT Settings
            var bindJwtSettings = new JwtSettings(); // modelo de JwtSettings.cs
            //traeremos el bloque "JsonWebTokenKeys" de configuracion desde el archivo appsettings.cs
            Configuration.Bind("JsonWebTokenKeys",bindJwtSettings);

            //add Singleton of JWT Settings
            // Una única instancia del servicio es creada y utilizada durante toda la vida de la aplicación.
            /*
                eso significa que tendre acceso a los valos de appsentting.json - JsonWebTokenKeys
                y en los controllers se accedemos a los valores de esta forma
            private readonly JwtSettings _jwtSettings; //con readonly le estamos diciendo que solo podemos acceder una ves
            //constructor
             public AccountController(JwtSettings jwtSettings)
            {
            _jwtSettings = jwtSettings;
            *********
            *hacer un Console.WriteLine(_jwtSettings)

            */
            Services.AddSingleton(bindJwtSettings);

            Services
                .AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;//nuesta app utilizara un sistema de autinicacion JWT - autenticar usuarios
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; //comprobar usuarios
                }).AddJwtBearer(
                options =>
                {
                    //configuraciones base
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true; //guaradaremos el token
                    options.TokenValidationParameters = 
                        new TokenValidationParameters()
                        {
                            ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
                            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),//issuerSigningKey Clave de firma del emisor, esta en appsettings.json
                            ValidateIssuer = bindJwtSettings.ValidateIssuer,
                            ValidIssuer = bindJwtSettings.ValidIssuer,
                            ValidateAudience = bindJwtSettings.ValidateAudience,
                            ValidAudience = bindJwtSettings.ValidAudience,
                            RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                            ValidateLifetime = bindJwtSettings.ValidateLifetime,
                            ClockSkew = TimeSpan.FromDays(1) //establece el margen de tiempo permitido para la sincronización entre el reloj del servidor y el reloj del emisor del token en un día (24 horas).


                        };

                });
        }
        
    }
}
