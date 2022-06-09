using API.Context;
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
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost("Register")]
        public ActionResult Post(Register register)
        {

            //Validate email
            if (employeeRepository.ValidateEmail(register.Email))
            {
                return StatusCode(400, new {status = HttpStatusCode.BadRequest, message = "Email already Exist"});
            }

            //validate phone 
            if (employeeRepository.ValidatePhone(register.Phone))
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Phone already Exist" });
            }

            //Insert
            var result = employeeRepository.Insert(register);
            if (result == 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Insert Failed" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, Message = "Insert Success" });
            }
            
        }
        [Authorize(Roles = "Director")]
        [HttpGet("GetRegisteredData")]
        public ActionResult GetRegisteredData()
        {
            return Ok(employeeRepository.GetRegisterdData());
        }

        [HttpGet("TestCors")]
        public ActionResult TestCors()
        {
            return Ok("Test Cors Success");
        }
    }
}
