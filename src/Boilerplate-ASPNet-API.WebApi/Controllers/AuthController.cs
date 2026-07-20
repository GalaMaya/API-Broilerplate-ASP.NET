using Microsoft.AspNetCore.Mvc;
using Boilerplate_ASPNet_API.Application.DTOs;
using Boilerplate_ASPNet_API.Application.Services;
using Boilerplate_ASPNet_API.WebApi.Common.Helpers;

namespace Boilerplate_ASPNet_API.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")] // Route otomatis menjadi: /api/auth
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    // ✅ BEST PRACTICE: Inject Interface, bukan concrete class
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // Route menjadi: POST /api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var loginResult = await _authService.LoginAsync(request);
            return Ok(ApiResponse<LoginResponse>.SuccessResponse(loginResult, "Login successful"));
        }
        catch (InvalidOperationException ex)
        {
            return Unauthorized(ApiResponse<object>.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<object>.ErrorResponse("Terjadi kesalahan internal pada server."));
        }
    }
}