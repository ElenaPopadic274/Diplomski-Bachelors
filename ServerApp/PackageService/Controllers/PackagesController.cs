using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PackageService.Interfaces;
using PackageService.DTO;
using Microsoft.AspNetCore.Authorization;

namespace PackageService.Controllers
{
    [Route("api/packages")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageService packageService;
        public PackagesController(IPackageService packageService)
        {
            this.packageService = packageService;
        }

        // GET: api/<PackagesController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(packageService.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            try
            {
                return Ok(packageService.FindById(id));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("put")]
        public ActionResult ModifyEntity([FromBody] PackageDto package)
        {
            try
            {
                return Ok(packageService.ChangeStatus(package ));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<PackageController>
        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public ActionResult Post([FromBody] PackageDto packet)
        {
            try
            {
                if (packageService.AddEntity(packet))
                    return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(new {e.Message,e.StackTrace,e.InnerException});
            }



            return BadRequest(false);
        }
    }
}
