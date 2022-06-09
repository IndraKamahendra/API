using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldEmployeesController : ControllerBase
    {
        private readonly OldEmployeeRepository employeeRepository;
        public OldEmployeesController(OldEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = employeeRepository.Get();
            if (result == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Empty Data" });
            }
            else
            {
                return Ok(result);
            }
            
        }

        [HttpPost]
        public ActionResult Post(Employee employee)
        {
            var result = employeeRepository.Insert(employee);
            if (result == 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Insert Failed" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, Message = "Insert Success" });
            }
        }

        [HttpPut]
        public ActionResult Put(Employee employee)
        {
            /*return Ok(employeeRepository.Update(employee));*/
            var result = employeeRepository.Update(employee);
            if (result == 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Update Failed" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, Message = "Update Success" });
            }
        }
        
        [HttpDelete("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            /*return Ok(employeeRepository.Delete(NIK));*/
            var result = employeeRepository.Delete(NIK);
            if (result == 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Delete Failed" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, Message = "Delete Success" });
            }
        }

        [HttpGet("{NIK}")]
        public ActionResult Get(string NIK)
        {
            Employee result = employeeRepository.Get(NIK);
            if (result == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "NIK Not Found" });
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("GetSalary")]
        public ActionResult GetSalary()
        {
            /*return Ok(employeeRepository.GetSalary()); */
            var result = employeeRepository.GetSalary();
            if (result == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Get Salary Failed" });
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("GetFirst/{Salary}")]
        public ActionResult GetFirst(int Salary)
        {
            /*return Ok(employeeRepository.GetFirst(Salary));*/
            var result = employeeRepository.GetFirst(Salary);
            if (result == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Salary Not Found" });
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("GetFirstOrDefault/{Salary}")]
        public ActionResult GetFirstOrDefault(int Salary)
        {
            var result = employeeRepository.GetFirstOrDefault(Salary);
            if (result == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Salary Not Found" });
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("single/{FirstName}")]
        public ActionResult GetSingle(string FirstName)
        {
            var result = employeeRepository.GetSingle(FirstName);
            if (result == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "FirstName Not Found" });
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("singleordefault/{FirstName}")]
        public ActionResult GetSingleOrDefault(string FirstName)
        {
            var result = employeeRepository.GetSingleOrDefault(FirstName);
            if (result == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "FirstName Not Found" });
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
