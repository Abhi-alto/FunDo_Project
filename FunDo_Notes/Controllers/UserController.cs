using BuisnessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FunDo_Notes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserBL userBL;
        private IConfiguration _config;
        public FunDoContext funDoContext;
        public UserController(IUserBL userBL, IConfiguration config, FunDoContext funDoContext)
        {
            this.userBL = userBL;
            this._config = config;
            this.funDoContext = funDoContext;
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
                return this.BadRequest(new { success = false, Token = token, message = $"Email not found...Register yourself" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("Forget Password/{email}")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                bool token = this.userBL.ForgetPassword(email);
                if (token != false)
                {
                    return this.Ok(new { success = true, status = 200, message = $"Reset Password link sent to the email id - {email}" });
                }
                return this.BadRequest(new { success = false, message = $"Reset password link not sent" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(PasswordModel passwordModel)
        {
            try
            {
                if (passwordModel.NewPassword != passwordModel.CPassword)
                {
                    return this.BadRequest(new { success = false, message = "New Password and Confirm Password are not equal." });
                }
                //Authorization, match email from token
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                var result = funDoContext.Users.Where(u => u.UserId == UserID).FirstOrDefault();
                string Email = result.email.ToString();
                bool res = this.userBL.ResetPassword(Email, passwordModel);
                if (res == false)
                {
                    return this.BadRequest(new { success = false, message = "Password not updated" });
                }
                return this.Ok(new { success = true, status = 200, message = "Password changed successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
