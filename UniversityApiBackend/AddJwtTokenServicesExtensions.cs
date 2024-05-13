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
            Configuration.Bind("JsonWebTokenKeys",bindJwtSettings);

            //add Singleton of JWT Settings
            Services.AddSingleton(bindJwtSettings);

            Services.AddAuthentication(
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
                            ClockSkew = TimeSpan.FromDays(1)


                        };

                });
        }
        
    }
}
