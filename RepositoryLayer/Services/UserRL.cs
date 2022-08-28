using CommonLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        FunDoContext fundoContext;
        private IConfiguration _config;
        public UserRL(FunDoContext fundoContext,IConfiguration config)
        {
            this.fundoContext = fundoContext;
            this._config=config;
        }

        public string LoginUser(LoginModel loginModel)
        {
            try
            {
                var user = fundoContext.Users.Where(x => x.email == loginModel.Email && x.password == loginModel.Password).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                return GenerateJwtToken(user.email,user.password);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateJwtToken(string email, string password)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  null,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Register(UserPostModel userPostModel)
        {
            try
            {
                User USER = new User();
                USER.FirstName = userPostModel.FirstName;
                USER.LastName = userPostModel.LastName;
                USER.email = userPostModel.email;
                USER.password = userPostModel.password;
                USER.CreatedDate = DateTime.Now;
                USER.modifyDate = DateTime.Now;
                fundoContext.Users.Add(USER);
                fundoContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
