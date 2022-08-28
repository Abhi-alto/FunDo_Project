using BuisnessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace FunDo_Notes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserBL userBL;
        private IConfiguration _config;
        public UserController(IUserBL userBL, IConfiguration config)
        {
            this.userBL = userBL;
            this._config = config;
        }
        [HttpPost("Register")]
        public IActionResult Register(UserPostModel userPostModel)
        {
            try
            {
                this.userBL.Register(userPostModel);
                return this.Ok(new {sucess=true,status=200,message=$"Registartion successful for {userPostModel.email}"});
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("LoginUser")]
        public IActionResult LoginUser(LoginModel loginModel)
        {
            try
            {
                string token = this.userBL.LoginUser(loginModel);
                if (token != null)
                {
                    return this.Ok(new { success = true, status = 200, Token = token, message = $"Login successful for {loginModel.Email}" });
                }
                return this.Ok(new { success = false, Token = token, message = $"Email not found...Register yourself" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
