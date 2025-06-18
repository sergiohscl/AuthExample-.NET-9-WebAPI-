using System;

namespace AuthExample.DTO;

public class RefreshTokenDto
{
    public string Username { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
