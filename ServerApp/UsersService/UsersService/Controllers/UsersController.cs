using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UsersService.DTO;
using UsersService.Interfaces;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
//using Newtonsoft.Json.Linq;

namespace UsersService.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Get()
        {
            //principal, clames principal
            try
            {
                return Ok(_userService.GetUsers());
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult Get(long id)
        {
            try
            {
                return Ok(_userService.FindById(id));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult Post([FromBody] UserDto user)
        {
            if (_userService.AddUser(user))
                return Ok(true);


            return Ok(false);
        }
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto user)
        {
            return Ok(_userService.Login(user));
        }

        [HttpPost("put")]
        [Authorize]
        public ActionResult Put([FromBody] UserDto user)
        {
            if (_userService.ModifyUser(user))
                return Ok();


            return BadRequest();
        }

        private string HasToken(string name, List<Claim> claims)
        {
            foreach (var item in claims)
            {
                if (item.Type == name)
                    return item.Value;
            }
            return null;
        }
        // PUT api/<UsersController>/5
        [HttpGet("username/{username}")]
        [Authorize]
        public ActionResult GetByUsername(string username)
        {
            UserDto user = _userService.FindByUsername(username);
            user.Password = "";
            if (user != null)
                return Ok(user);
            return BadRequest();
        }
        [HttpGet("Unactivated")]
        [Authorize(Roles = "Admin")]
        public ActionResult Unactivated()
        {
            List<UserDto> ret = _userService.Unactivated();
            return Ok(ret);

        }
        [HttpPost("verifyUser")]
        [Authorize(Roles = "Admin")]
        public ActionResult VerifyUser([FromBody] long id)
        {
            bool ret = _userService.VerifyUser(id);
            return Ok(ret);

        }
        [HttpPost("dismissUser")]
        [Authorize(Roles = "Admin")]
        public ActionResult DismissUser([FromBody] long id)
        {
            bool ret = _userService.DismissUser(id);
            return Ok(ret);

        }
    }
}
