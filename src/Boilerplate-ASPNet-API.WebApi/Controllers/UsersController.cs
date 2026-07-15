using Microsoft.AspNetCore.Mvc;
using Boilerplate_ASPNet_API.Application.DTOs;
using Boilerplate_ASPNet_API.Application.Services;
using Boilerplate_ASPNet_API.WebApi.Common.Helpers;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Boilerplate_ASPNet_API.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

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
}