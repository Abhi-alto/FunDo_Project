using CommonLayer;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        FunDoContext fundoContext;
        public UserRL(FunDoContext fundoContext)
        {
            this.fundoContext = fundoContext;
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
