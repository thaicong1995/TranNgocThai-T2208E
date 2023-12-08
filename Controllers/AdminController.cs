using Exercise.Dto;
using Exercise.Models;
using Exercise.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.TokenConfig;

namespace Exercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly Token _token;
        public AdminController( IAdminService adminService, Token token)
        {
            _adminService = adminService;
            _token = token;
        }

      
        [HttpPost]
        [Route("CreateNewUser")]
        public async Task<IActionResult> CreateNewUser(UserDto userDto)
        {
            try
            {
                var newUser = _adminService.Register(userDto);
                if (newUser != null)
                {
                    return Ok(newUser);
                }
                return BadRequest("User create fail");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }


       
        [HttpPost]
        [Route("Login")]
        public ActionResult<Admin> Login(UserDto userDto)
        {
            try
            {
                Admin user = _adminService.Login(userDto);

                string jwtToken = _token.CreateToken(user);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddMinutes(30)
                };

                HttpContext.Response.Cookies.Append("authenticationToken", jwtToken, cookieOptions);
                return Ok(jwtToken);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
    }
}
