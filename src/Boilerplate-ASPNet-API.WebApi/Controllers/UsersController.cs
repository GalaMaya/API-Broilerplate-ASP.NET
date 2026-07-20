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
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
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
}