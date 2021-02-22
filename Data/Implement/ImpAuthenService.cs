using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Models;
using System;
using Microsoft.Extensions.Configuration;
using System.Linq;
using API.Encode;

namespace API.Data
{
    public class ImpAuthenService : IAuthenService
    {
        private readonly NorthwindContext db;
        private readonly HashService hash;

        public ImpAuthenService(IConfiguration configuration, NorthwindContext _db, HashService _hash)
        {
            Configuration = configuration;
            db = _db;
            hash = _hash;
        }

        private IConfiguration Configuration { get; }

        public User AuthenticateUser(User login)
        {
            // User userlogin = null;
            // if (login.UserID == "admin" && login.Password == "123")
            // {
            //     userlogin = new User { UserID = "admin", Password = "123", Email = "xx@xx.com" };

            // }
            User userlogin = new User();
            try
            {
                userlogin = db.User.FirstOrDefault(u => u.UserID == login.UserID);
                if (userlogin != null)
                {
                    string PassValue = hash.ComputeStringToSha512Hash(login.Password);
                    if (PassValue == userlogin.Password)
                    {
                        return userlogin;
                    }
                }
                return userlogin = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string GennerateJSONWebToken(User userinfo)
        {

            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userinfo.UserID),
                new Claim(JwtRegisteredClaimNames.Email,userinfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: Configuration["JWT:Issuer"],
                audience: Configuration["JWT:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: credentials
            );

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
    }
}