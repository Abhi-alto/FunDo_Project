using CommonLayer;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
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
                return GenerateJwtToken(user.email,user.UserId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateJwtToken(string email, int UserId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("ThisIsMYSecretKey");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", email),
                    new Claim("UserId",UserId.ToString()),
                    }),
                    Expires
                    = DateTime.UtcNow.AddHours(2),

                    SigningCredentials =
                    new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
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

        public bool ForgetPassword(string email)
        {
            try
            {
                var user = fundoContext.Users.Where(x => x.email == email).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                MessageQueue FundoQ = new MessageQueue();

                //Setting the QueuPath where we want to store the messages.
                FundoQ.Path = @".\private$\FunDo_Notes";
                if(MessageQueue.Exists(FundoQ.Path))
                {
                    //Exists
                    FundoQ = new MessageQueue(@".\Private$\FunDo_Notes");
                }
                else
                {
                    // Creates the new queue named "Bills"
                    MessageQueue.Create(FundoQ.Path);
                }
                Message MyMessage = new Message();
                MyMessage.Formatter = new BinaryMessageFormatter();
                MyMessage.Body = GenerateJwtToken(email, user.UserId);
                MyMessage.Label = "Forget Password Email";
                FundoQ.Send(MyMessage);
                Message msg = FundoQ.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendEmail(email, msg.Body.ToString(),user.FirstName);
                FundoQ.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                FundoQ.BeginReceive();
                FundoQ.Close();


                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()), e.Message.ToString());
                queue.BeginReceive();

            }
            catch (MessageQueueException ex)

            {

                if (ex.MessageQueueErrorCode ==

                MessageQueueErrorCode.AccessDenied)

                {

                    Console.WriteLine("Access is denied. " +

                    "Queue might be a system queue.");

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateToken(string email)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("ThisIsMYSecretKey");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", email)
                    }),
                    Expires
                    = DateTime.UtcNow.AddHours(2),

                    SigningCredentials =
                         new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassword(string email, PasswordModel passwordModel)
        {
            try
            {
                var user = fundoContext.Users.Where(x => x.email == email).FirstOrDefault();
                if (passwordModel.NewPassword != passwordModel.CPassword)
                {
                    return false;
                }
                user.password= passwordModel.NewPassword;
                fundoContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
