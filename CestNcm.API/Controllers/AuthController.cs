using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CestNcm.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IConfiguration configuration) : ControllerBase
{
    private readonly IConfiguration _configuration = configuration;

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        if (request.Username != "admin" || request.Password != "123456") { return Unauthorized(); }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});
    }
}

/// <summary>
/// Represents a request for user login.
/// This record is used to encapsulate the username and password required for authentication.
/// </summary>
/// <param name="Username"></param>
/// <param name="Password"></param>
public record LoginRequest(
    [Required(ErrorMessage = "Usuário é obrigatório")] string Username,
    [Required(ErrorMessage = "Senha é obrigatória")] string Password
);
// This controller provides a simple authentication endpoint for demonstration purposes.
// In a real application, you would implement proper user management and security practices.