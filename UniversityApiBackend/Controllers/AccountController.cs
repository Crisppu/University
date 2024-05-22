using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;
//este controllaor se hizo desde cero

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")] //se add [action] para que sea controlada desde la ruta
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings; //con readonly le estamos diciendo que solo podemos acceder una ves
        //constructor
        public AccountController(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        private IEnumerable<User> Logins = new List<User>()
        {
            new User() { 
                Id = 1,
                Email = "criyat@gmail.com",
                Name = "Admin",
                Password = "Admin"
            },
            new User() {
                Id = 2,
                Email = "criyat2@gmail.com",
                Name = "User1",
                Password = "pepe"
            }
        };
        //IActionResult representar el resultado de una acción de un controlador.
        [HttpPost]
        public IActionResult GetToken(UserLogins userLogins)
        {
            try
            {
                var Token = new UserTokens();
                //Any hace una comparar o una busqueda que coincida. busqueda en el array-Logins{Name} y el parametro userLogins.UserName
                var Valid = Logins.Any(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase)); //StringComparison.OrdinalIgnoreCase - no cuenta las mayusculas ni minusculas
                if (Valid)
                {
                    //busqueda de la primera persona que concida, en este caso si existen dos con el mismo nombre retornara el primero de la lista
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                    Token = JwtHelpers.GetTokenKey(new UserTokens()
                    {
                        UserName = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidId = Guid.NewGuid()
                    }, _jwtSettings);
                    

                }
                else
                {
                    return BadRequest("credenciales incorrectos");
                }
                return Ok(Token);

            }
            catch(Exception ex)
            {
                throw new Exception( "error",ex);
            }
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes =  JwtBearerDefaults.AuthenticationScheme, Roles ="Administrator")] // se add "Administrator" porque el archivo JwtHelpers.cs se valido asi
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }
    }
}
