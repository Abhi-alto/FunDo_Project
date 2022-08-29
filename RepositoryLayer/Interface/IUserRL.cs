using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        void Register(UserPostModel userPostModel);
        public string LoginUser(LoginModel loginModel);
        public bool ForgetPassword(string email);    
    }
}
