using System;
using AuthExample.Models.DTO;

namespace AuthExample.Services.Interfaces;

public interface IUserService
{
    void Register(UserRegisterDto dto);
    (string accessToken, string refreshToken) Login(UserLoginDto dto);
    (string accessToken, string refreshToken) Refresh(string username, string refreshToken);
    List<object> GetAllUsers();
}