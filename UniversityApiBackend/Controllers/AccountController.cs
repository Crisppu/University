using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
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
        private readonly UniversityDBContext _context;

        //constructor
        public AccountController(JwtSettings jwtSettings, UniversityDBContext context)
        {
            _jwtSettings = jwtSettings;
            _context = context;
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
            },
        };
        // GET: api/Users
        /*usando funciones async utilizaremos Task<> */
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
        //IActionResult representar el resultado de una acción de un controlador.
        [HttpPost]
        public IActionResult GetToken(UserLogins userLogins)
        {
            try
            {
                //uso de linQ
                var searchUser = (from user in _context.Users
                                  where user.LastName == userLogins.UserName && user.Password == userLogins.Password
                                  select user).FirstOrDefault();
                var Token = new UserTokens();

                if (searchUser != null)
                {
                    Token = JwtHelpers.GetTokenKey(new UserTokens()
                    {
                        UserName = searchUser.Name,
                        EmailId = searchUser.Email,
                        Id = searchUser.Id,
                        GuidId = Guid.NewGuid()
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("credenciales incorrectos");
                }

                //Any hace una comparar o una busqueda que coincida. busqueda en el array-Logins{Name} y el parametro userLogins.UserName
                /* var Valid = Logins.Any(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase)); //StringComparison.OrdinalIgnoreCase - no cuenta las mayusculas ni minusculas
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
                */
                return Ok(Token);

            }
            catch(Exception ex)
            {
                throw new Exception( "error",ex);
            }
        }
        //el rol se le asigna en JwtHelpers - GetClaims
        [HttpGet]
        [Authorize(AuthenticationSchemes =  JwtBearerDefaults.AuthenticationScheme, Roles ="Administrator")] // se add "Administrator" porque el archivo JwtHelpers.cs se valido asi
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }

        /* pendiente mio
        [HttpPost]
        public async Task<IActionResult> GetTokenDataBase(User user)
        {
            try
            {
                var seachUser = await _context.Users.ToListAsync();
                Console.WriteLine(seachUser);

                var Token = new UserTokens();
                //Any hace una comparar o una busqueda que coincida. busqueda en el array-Logins{Name} y el parametro userLogins.UserName
               // var Valid = Logins.Any(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase)); //StringComparison.OrdinalIgnoreCase - no cuenta las mayusculas ni minusculas
                if (true)
                {
                    //busqueda de la primera persona que concida, en este caso si existen dos con el mismo nombre retornara el primero de la lista
                    //var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
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
            catch (Exception ex)
            {
                throw new Exception("error", ex);
            }
        }*/
    }
}
