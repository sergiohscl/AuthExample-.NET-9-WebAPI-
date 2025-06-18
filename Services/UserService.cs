using System;
using AuthExample.Data;
using AuthExample.Exceptions;
using AuthExample.Models;
using AuthExample.Models.DTO;
using AuthExample.Services.Interfaces;

namespace AuthExample.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context) => _context = context;

    public void Register(UserRegisterDto dto)
    {
        PasswordValidator.Validate(dto.Password);

        if (_context.Users.Any(u => u.Username == dto.Username))
            throw new UserAlreadyExistsException("Usuário já existe.");

        var user = new User
        {
            Username = dto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = dto.Role
        };

        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public (string accessToken, string refreshToken) Login(UserLoginDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == dto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            throw new UnauthorizedAccessException("Credenciais inválidas.");

        var token = TokenService.GenerateToken(user.Username, user.Role);
        var refreshToken = TokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        _context.SaveChanges();

        return (token, refreshToken);
    }

    public (string accessToken, string refreshToken) Refresh(string username, string refreshToken)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Token inválido ou expirado.");

        var newToken = TokenService.GenerateToken(user.Username, user.Role);
        var newRefresh = TokenService.GenerateRefreshToken();

        user.RefreshToken = newRefresh;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        _context.SaveChanges();

        return (newToken, newRefresh);
    }

    public List<object> GetAllUsers()
    {
        var users = _context.Users
            .Select(u => new { u.Id, u.Username, u.Role, u.RefreshTokenExpiry })
            .ToList<object>();

        if (!users.Any())
            throw new NotFoundException("Nenhum usuário encontrado.");

        return users;
    }
}