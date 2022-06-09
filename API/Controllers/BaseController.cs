using API.Repository.Interface;
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
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public ActionResult<Entity> Get()
        {
            return Ok(repository.Get());
        }
        [HttpGet("{key}")]
        public ActionResult<Entity> Get(Key key)
        {
            /*return Ok(repository.Get(key));*/
            var result = repository.Get(key);
            if (result == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "NIK Not Found" });
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpPost]
        public ActionResult<Entity> Post(Entity entity)
        {
            var result = repository.Insert(entity);
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
        public ActionResult<Entity> Put(Entity entity)
        {
            var result = repository.Update(entity);
            if (result == 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Update Failed" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, Message = "Update Success" });
            }
        }
        [HttpDelete("{key}")]
        public ActionResult<Entity> Delete(Key key)
        {
            var result = repository.Delete(key);
            if (result == 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, Message = "Delete Failed" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, Message = "Delete Success" });
            }
        }
    }
}
