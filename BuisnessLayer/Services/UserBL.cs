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
    }
}
