using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Interface
{
    public interface IUserBL
    {
        void Register(UserPostModel userPostModel);
        public string LoginUser(LoginModel loginModel);
        public bool ForgetPassword(string email);
        public bool ResetPassword(string email, PasswordModel password);
    }
}
