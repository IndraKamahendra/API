using API.Models;
using API.Models.ViewModels;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public AccountsController (AccountRepository accountRepository) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [HttpPost("Login")]
        public ActionResult Post(Login login)
        {
            string Token;
            int tokenLogin = accountRepository.Login(login, out Token);

            if (accountRepository.Login(login, out Token) == 200)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, Token, Message = "Loggin" });
            }
            else if (accountRepository.Login(login, out Token) == 404)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, Token, Message = "Email Incorect" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Token, Message = "Password Incorect" });
            }
        }

        [HttpPost("ForgotPassword")]
        public ActionResult Post(ForgotPassword forgotPassword)
        {
            var result = accountRepository.ForgotPassword(forgotPassword);
            if (result == -1)
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, Message = "Email tidak ada" });
            }
            else if (result > 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, Message = "Kode OTP sudah dikirim" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Kode OTP sudah dikirim" });
            }
        }

        [HttpPost("ChangePassword")]
        public ActionResult Post(ChangePassword changePassword)
        {
            var result = accountRepository.ChangePassword(changePassword);
            if (result == -1)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Email Salah" });
            }
            else if (result == -2)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "OTP Telah Kadaluarsa" });
            }
            else if (result == -3)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "OTP Salah!" });
            }
            else if (result == -4)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Account tidak aktif" });
            }
            else if (result > 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.BadRequest, Message = "Pengubahan password berhasil dilakukan" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Pengubahan password gagal dilakukan" });
            }
        }
        [Authorize]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("test JWT Berhasil");
        }

    }
}
