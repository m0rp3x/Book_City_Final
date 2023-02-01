﻿using System.ComponentModel.DataAnnotations;
namespace WebApplication2.Models;

public class RegistrationDB
{
    [Required(ErrorMessage ="Не указан Email")]
    public string Email { get; set; }
         
    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
         
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароль введен неверно")]
    public string ConfirmPassword { get; set; }
    
  
    
    
}