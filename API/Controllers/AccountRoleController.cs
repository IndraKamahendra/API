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
    public class AccountRoleController : BaseController<AccountRole, AccountRoleRepository, string>
    {
        private readonly AccountRoleRepository accountRoleRepository;
        public AccountRoleController(AccountRoleRepository accountRoleRepository) : base(accountRoleRepository)
        {
            this.accountRoleRepository = accountRoleRepository;
        }
        [Authorize(Roles = "Director")]
        [HttpPost("SignManager")]
        public ActionResult SignManager(SignManager signManager)
        {
            var result = accountRoleRepository.SignManager(signManager);
            if (result == 0)
            {
                return StatusCode(403, new { status = HttpStatusCode.BadRequest, Message = "Sign Failed" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, Message = "Sign Succes" });
            }
        }
    }
}
