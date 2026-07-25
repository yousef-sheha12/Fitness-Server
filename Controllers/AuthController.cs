using Fitness.Helpers;
using Fitness.Interface.IService;
using Fitness.Models.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtHelper _jwtHelper;

        public AuthController(IUserService userService, JwtHelper jwtHelper)
        {
            _userService = userService;
            _jwtHelper = jwtHelper;
        }

        private IActionResult ApiResponse(object? data = null, string message = "Success", int statusCode = 200)
        {
            return StatusCode(statusCode, new { success = true, message, data });
        }

        [HttpPost("api/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _userService.LoginAsync(loginDto);
            if (result == null)
                return ApiResponse(null, "Invalid credentials", 401);
            return ApiResponse(result, "Login successful");
        }

        [HttpPost("api/register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _userService.RegisterAsync(registerDto);
            if (result == null)
                return ApiResponse(null, "Email already exists", 400);
            return ApiResponse(result, "Registration successful");
        }

        [HttpPost("api/verify-otp")]
        public IActionResult VerifyOtp([FromQuery] string email, [FromQuery] string code)
        {
            return ApiResponse(message: "OTP verified");
        }

        [HttpPost("api/forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            await _userService.ForgotPasswordAsync(dto.Email);
            return ApiResponse(message: "Password reset email sent");
        }

        [HttpPost("api/reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] string email, [FromQuery] string code, [FromQuery] string newPassword)
        {
            var result = await _userService.ResetPasswordAsync(email, code, newPassword);
            if (!result) return ApiResponse(null, "Invalid reset code", 400);
            return ApiResponse(message: "Password reset successful");
        }

        [HttpPost("api/logout")]
        public IActionResult Logout()
        {
            return ApiResponse(message: "Logged out successfully");
        }

        [HttpGet("api/auth/google/redirect")]
        public IActionResult GoogleRedirect()
        {
            return ApiResponse(new { url = "#" });
        }
    }
}
