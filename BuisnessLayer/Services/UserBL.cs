using BuisnessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public bool ForgetPassword(string email)
        {
            try
            {
                return userRL.ForgetPassword(email);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string LoginUser(LoginModel loginModel)
        {
            try
            {
                return userRL.LoginUser(loginModel);
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
                this.userRL.Register(userPostModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassword(string email, PasswordModel passwordModel)
        {
            try
            {
                return userRL.ResetPassword(email,passwordModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
