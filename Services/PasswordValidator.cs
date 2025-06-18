using System;
using System.Text.RegularExpressions;
using AuthExample.Exceptions;

namespace AuthExample.Services;

public static class PasswordValidator
{
    public static void Validate(string password)
    {
        // Exatamente 6 dígitos numéricos
        if (!Regex.IsMatch(password, @"^\d{6}$"))
            throw new InvalidPasswordException("A senha deve conter exatamente 6 dígitos numéricos.");

        // Sequência crescente
        if (IsSequential(password))
            throw new InvalidPasswordException("A senha não pode ser uma sequência numérica.");

        // Dígitos repetidos
        if (password.Distinct().Count() == 1)
            throw new InvalidPasswordException("A senha não pode ter todos os dígitos repetidos.");
    }

    private static bool IsSequential(string password)
    {
        for (int i = 1; i < password.Length; i++)
        {
            if (password[i] != password[i - 1] + 1)
                return false;
        }
        return true;
    }
}