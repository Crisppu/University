using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Helpers
{
    //JSON Web Token - Jwt
    public static class JwtHelpers //static para que no se pueda hacer nada con este archivo
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
        {
            List<Claim> claims= new List<Claim>
            {
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name,userAccounts.UserName),
                new Claim(ClaimTypes.Email, userAccounts.EmailId),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
            if(userAccounts.UserName == "Admin" )
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));

            }
            else if(userAccounts.UserName == "User 1")
            {
                claims.Add(new Claim(ClaimTypes.Role,"User"));
                claims.Add(new Claim("UserOnly","User 1"));
            }
            return claims;
        }
        public static IEnumerable<Claim> GetClaims(this UserTokens userTokens, out Guid Id)
        {
            Id = Guid.NewGuid(); //genera un nuevo id
            return GetClaims(userTokens,Id);
        }
        public static UserTokens GetTokenKey(UserTokens model,JwtSettings jwtSettings) {
            try
            {
                var userToken = new UserTokens();
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }
                //obtain SECRET KEY - clave secreta
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                
                Guid Id;
                //el tiempo que expira es de un dia
                DateTime expireTime = DateTime.UtcNow.AddDays(1);
                //validar of out token - verificar cuanto tiene de valides el token
                userToken.Validity = expireTime.TimeOfDay;
                // GENERATE our JWT - generamos nuestro token
                var jwToken = new JwtSecurityToken(
                        issuer: jwtSettings.ValidIssuer,
                        audience: jwtSettings.ValidAudience,
                        claims: GetClaims(model, out Id),
                        notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                        expires: new DateTimeOffset(expireTime).DateTime,
                        signingCredentials: new SigningCredentials( //estructura de firma
                                new SymmetricSecurityKey(key), //pasaremos la clave secreta
                                SecurityAlgorithms.HmacSha256 //cifra nuestra informacion.
                            )
                        
                        );
                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                userToken.UserName = model.UserName;
                userToken.Id = model.Id;
                userToken.GuidId = Id;
                return userToken;

            }catch (Exception ex)
            {
                throw new Exception("error Generating the JWT", ex);
            }
            
        }
    }
}
