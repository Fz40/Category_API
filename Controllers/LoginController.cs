using System;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Authorize]
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IAuthenService _Authen;
        public LoginController(IAuthenService Authen)
        {
            _Authen = Authen;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(User login)
        {
            try
            {
                IActionResult response = Unauthorized();

                var user = _Authen.AuthenticateUser(login);
                if (user != null)
                {
                    var tokenStr = _Authen.GennerateJSONWebToken(user);
                    response = Ok(new { token = tokenStr });
                }

                return response;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}