using Microsoft.AspNetCore.Mvc;
using Nettbutikk.Data.DTO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Nettbutikk.Data.Services;
using Newtonsoft.Json;

namespace Nettbutikk.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthController : ControllerBase
    {
        private readonly IdentityService _identityService;
        private readonly UserContextService _userContextService;

        public AuthController(IdentityService identityService, UserContextService userContextService)
        {
            _identityService = identityService;
            _userContextService = userContextService;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                await _identityService.CreateUserOnRegister(registerDTO);

                return Ok();
            }

            catch(Exception e)
            {
                if (e.Message.Equals("Username taken."))
                    return StatusCode(403, e.Message);

                if (e.Message.Equals("Password rules violation."))
                    return StatusCode(404, e.Message);

                if (e.Message.Equals("Creation failed."))
                    return StatusCode(500, e.Message + " Something went wrong on the server. Try again.");

                if (e.Message.Equals("Invalid role code."))
                    return StatusCode(404, e.Message);

                else return StatusCode(404, e.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var token = await _identityService.LoginUser(loginDTO);

                return Ok(token);
            }

            catch(Exception e)
            {
                if (e.Message.Equals("Username does not exist."))
                    return StatusCode(404, "Username does not exist. Try again with valid username.");

                if (e.Message.Equals("Login failed."))
                    return StatusCode(404, "Login has failed.");

                if (e.Message.Equals("Incorrect password."))
                    return StatusCode(404, "Incorrect password. Type in correct password.");

                else return StatusCode(404, e.Message);
            }
        }

        [HttpPut]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var user = await _userContextService.GetCurrentUserOnHttpContext(HttpContext)
                    ?? throw new Exception("User not found");

                await _identityService.LogoutUser(user);
                return Redirect("https://localhost:44340/login");
            }

            catch(Exception e)
            {
                if (e.Message.Equals("User not found"))
                    return StatusCode(404, "User could not be found. Check if authenticated and try again or contact customer service.");

                else return StatusCode(404, e.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("deleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] UsernameIdentificationDTO dto)
        {
            try
            {
                var user = await _userContextService.GetCurrentUserOnHttpContext(HttpContext)
                    ?? throw new Exception("User not found");

                var receipt = _identityService.DeleteUser(dto.Username, user);
                var response = JsonConvert.SerializeObject(receipt);
                return Ok(response);
            }

            catch(Exception e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}