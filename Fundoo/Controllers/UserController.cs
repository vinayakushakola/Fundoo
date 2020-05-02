using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Models;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Fundoo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _signUpBusiness;
        private IConfiguration _config;

        public UserController(IUserBusiness signUpBusiness, IConfiguration config)
        {
            _signUpBusiness = signUpBusiness;
            _config = config;
        }

        [HttpGet, Authorize]
        public ActionResult GetUsersData()
        {
            try
            {
                List<ResponseModel> userData = _signUpBusiness.GetUsersData();
                return Ok(userData.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

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
                    var jsonToken = CreateToken(data, "login");
                    return Ok(new { success, message, data, jsonToken });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        public string CreateToken(ResponseModel userToken, string type)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Issuer"],
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
