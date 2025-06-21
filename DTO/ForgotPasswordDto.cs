using System;

namespace AuthExample.DTO;

public class ForgotPasswordDto
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}