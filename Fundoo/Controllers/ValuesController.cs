using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Models;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace Fundoo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUserBusiness _signUpBusiness;
        public ValuesController(IUserBusiness signUpBusiness)
        {
            _signUpBusiness = signUpBusiness;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [Route("SignUp")]
        public IActionResult CreateAccount(SignUpRequestModel signUpRequest)
        {
            try
            {
                ResponseModel data = _signUpBusiness.CreateAccount(signUpRequest);
                bool success = false;
                string message;
                if (data == null)
                {
                    message = "No Data Found!";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    string userFullName = data.FirstName + data.LastName;
                    message = "Hello " + userFullName + ", Your Account Created Successfully";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult UserLogin(LoginRequest login)
        {
            try
            {
                ResponseModel data = _signUpBusiness.UserLogin(login);
                bool success = false;
                string message;
                if (data == null)
                {
                    message = "No Data Found!";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    string userFullName = data.FirstName + " " + data.LastName;
                    message = "Hello " + userFullName + ", You Logged in Successfully";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
