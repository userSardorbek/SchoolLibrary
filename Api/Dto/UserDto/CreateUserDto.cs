﻿namespace WebApplication1.Dto;

public class CreateUserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}