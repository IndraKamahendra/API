using API.Context;
using API.Models;
using API.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext context;
        public IConfiguration _configuration;
        public AccountRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.context = myContext;
            this._configuration = configuration;
        }

        public bool ValidateEmail(string email)
        {
            var emp = context.Employees.FirstOrDefault(emp => emp.Email == email);
            return emp != null;
        }
        public bool ValidatePassword(string password, string correct)
        {
            return BCrypt.Net.BCrypt.Verify(password, correct);
        }
        private string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }
        public int Login(Login login, out string Token)
        {
            if (ValidateEmail(login.Email))
            {
                var check = (from s in context.Employees
                             join a in context.Accounts
                             on s.NIK equals a.NIK
                             where s.Email == login.Email
                             select a.Password).FirstOrDefault();

                var checkPassword = ValidatePassword(login.Password, check);

                if (checkPassword != false)
                {
                    /*var NIK = "";*/
                    var CheckRole = (from emp in context.Employees
                                     join acr in context.AccountRoles
                                     on emp.NIK equals acr.AccountId
                                     join r in context.Roles
                                     on acr.RoleId equals r.Id
                                     where emp.Email == login.Email
                                     select r).ToList();

                    var claims = new List<Claim>();
                    claims.Add(new Claim("Email", login.Email));
                    foreach ( var roles in CheckRole)
                    {
                        claims.Add(new Claim("roles", roles.Name));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var sigIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["JwtConstants:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: sigIn
                        );
                    var idToken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("Token Security", idToken.ToString()));

                    Token = idToken;
                    return 200;
                }
                Token = null;
                return 400;
            }
            Token = null;
            return 404;
        }

        public string getOtp()
        {
            Random random = new Random();
            string randomNumber = (random.Next(100000, 999999)).ToString();
            return randomNumber;
        }

        public void SendEmail(ForgotPassword forgotPassword, string otp)
        {
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("859fbf96e14491", "2530fce10e112b"),
                EnableSsl = true
            };
            /* client.Send("from@example.com", forgotVM.Email, "Kode OTP", "Kode OTP anda adalah :");*/
            MailMessage message = new MailMessage("admin@gmail.com", forgotPassword.Email);
            message.Subject = "Test OTP";
            message.Body = "OTP ANDA : " + otp;
            message.IsBodyHtml = true;
            client.Send(message);
        }

        public int ForgotPassword(ForgotPassword forgotPassword)
        {
            if (!ValidateEmail(forgotPassword.Email))
            {
                return -1;
            }
            else
            {
                string otp = getOtp();
                Account account = (from s in context.Employees
                                   join a in context.Accounts
                                   on s.NIK equals a.NIK
                                   where s.Email == forgotPassword.Email
                                   select a).FirstOrDefault();                

                account.IsActive = false;
                account.OTP = otp;
                account.ExpiredTime = DateTime.Now.AddMinutes(5);
                context.Entry(account).State = EntityState.Modified;
                var result = context.SaveChanges();
                SendEmail(forgotPassword, otp);
                return result;
            }
        }

        public int ChangePassword(ChangePassword changePassword)
        {
            var account = (from s in context.Employees
                           join a in context.Accounts
                           on s.NIK equals a.NIK
                           where s.Email == changePassword.Email
                           select a).FirstOrDefault();
            var checkEmail = ValidateEmail(changePassword.Email);

            if (checkEmail == false)
            {
                return -1;
            }
            else if (account.IsActive)
            {
                return -2;
            }
            else if (DateTime.Now > account.ExpiredTime)
            {
                return -3;
            }
            else if (account.OTP != changePassword.OTP)
            {
                return -4;
            }
            
            else
            {
                account.IsActive = true;
                account.Password = BCrypt.Net.BCrypt.HashPassword(changePassword.new_password, GetRandomSalt());

                context.Entry(account).State = EntityState.Modified;
                var result = context.SaveChanges();
                return result;
            }
        }

        /*public int InsertDataManager()
        {

        }*/
    }
}
