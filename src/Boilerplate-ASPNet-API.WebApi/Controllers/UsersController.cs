using Microsoft.AspNetCore.Mvc;
using Boilerplate_ASPNet_API.Application.DTOs;
using Boilerplate_ASPNet_API.Application.Services;
using Boilerplate_ASPNet_API.WebApi.Common.Helpers;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Authorization;

namespace Boilerplate_ASPNet_API.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly AuthService _authService;

    public UsersController(UserService userService, AuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        try
        {
            var createdUser = await _userService.CreateUserAsync(request);

            return CreatedAtAction(
                nameof(GetUserById),
                new { id = createdUser.Id },
                ApiResponse<UserResponseDto>.SuccessResponse(
                    new UserResponseDto(
                        createdUser.Id,
                        createdUser.Name,
                        createdUser.Email,
                        createdUser.Status,
                        createdUser.CreatedAt
                    ),
                    "User created successfully"
                )
            );
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request)
    {
        try
        {
            var updatedUser = await _userService.UpdateUserAsync(id, request);

            return Ok(ApiResponse<UserResponseDto>.SuccessResponse(updatedUser, "User updated successfully"));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<object>.ErrorResponse(ex.Message));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
        }
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllUser()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(ApiResponse<IEnumerable<UserResponseDto>>.SuccessResponse(
                users,
                "Success Get All Users"
            ));
        }
        catch(InvalidOperationException ex) {
            return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
        }
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);

            return Ok(ApiResponse<UserResponseDto>.SuccessResponse(
                new UserResponseDto(
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Status,
                    user.CreatedAt
                ),
                "User retrieved successfully"
            ));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ApiResponse<object>.ErrorResponse(ex.Message));
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id) 
    {
        try
        {
            var User = await _userService.GetUserByIdAsync(id);

            if(User == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse($"User with ID {id} not found."));
            }

            await _userService.DeleteUserAsync(id);

            return Ok(ApiResponse<object>.SuccessResponse(null, "User deleted successfully"));
        }
        catch (KeyNotFoundException ex) 
        {
            return NotFound(ApiResponse<object>.ErrorResponse(ex.Message));
        }
    }

    [HttpPost("login")] // Route: POST /api/users/login
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