using BuisnessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FunDo_Notes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
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
    }
}
