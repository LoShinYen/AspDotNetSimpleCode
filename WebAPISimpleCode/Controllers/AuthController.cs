using Microsoft.AspNetCore.Mvc;

namespace WebAPISimpleCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // POST: api/Auth/Register
        [HttpPost(Name = "Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _authService.CreateAccountAsync(request.Username, request.Password, request.Email))
            {
                return Ok(new { Message = "Account created successfully" });
            }

            return BadRequest(new { Message = "Account creation failed, username might already exist" });
        }

        // POST: api/Auth/Login
        [HttpPost(Name ="Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (await _authService.ValidateCredentialsAsync(request.Username, request.Password))
            {
                return Ok(new { Message = "Login successful" });
            }

            return Unauthorized(new { Message = "Invalid username or password" });
        }

        // PUT: api/Auth/Activate/{username}
        [HttpPut("activate/{username}")]
        public async Task<IActionResult> Activate(string username)
        {
            if (await _authService.ActivateAccountAsync(username))
            {
                return Ok(new { Message = "Account activated successfully" });
            }

            return NotFound(new { Message = "Account not found" });
        }

        // DELETE: api/Auth/Delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _authService.DeleteAccountAsync(id))
            {
                return Ok(new { Message = "Account deleted successfully" });
            }

            return NotFound(new { Message = "Account not found" });
        }
    }
}
