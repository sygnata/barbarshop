using Barbearia.Application.DTOs;
using Barbearia.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var token = _authService.Login(request.Email, request.Senha);
        if (token == null)
            return Unauthorized();
        return Ok(new LoginResponse { Token = token });
    }
}
