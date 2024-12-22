namespace MilGlorian.Application.DTOs.Auth;

public record RegisterDTO(string? FullName, string UserName, string Email, string Password);
