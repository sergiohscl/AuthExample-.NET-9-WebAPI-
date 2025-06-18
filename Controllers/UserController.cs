using System;
using AuthExample.DTO;
using AuthExample.Models.DTO;
using AuthExample.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthExample.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service) => _service = service;

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register(UserRegisterDto dto)
    {
        _service.Register(dto);
        return Ok(new { message = "Usuário registrado com sucesso." });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(UserLoginDto dto)
    {
        var (token, refresh) = _service.Login(dto);
        return Ok(new { token, refreshToken = refresh });
    }

   [AllowAnonymous]
    [HttpPost("refresh")]
    public IActionResult RefreshToken([FromBody] RefreshTokenDto dto)
    {
        var (newToken, newRefresh) = _service.Refresh(dto.Username, dto.RefreshToken);

        return Ok(new
        {
            token = newToken,
            refreshToken = newRefresh
        });
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        return Ok(_service.GetAllUsers());
    }
}