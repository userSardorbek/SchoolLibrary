﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dto;

public class LoginDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}