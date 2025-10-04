using ManagesMotorcycleRentals.Application.DTOs;
using ManagesMotorcycleRentals.Application.Services.Interfaces;
using ManagesMotorcycleRentals.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagesMotorcycleRentals.API.Controllers
{
    
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(Notify notify, IUserService userService) : base(notify)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("user/login")]       
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto, CancellationToken cancellationToken)
        {
            return ResponseData(
                await _userService.GenerateTokenAsync(loginDto.Id, loginDto.Role, cancellationToken)
            );
        }
    }
}
