﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication2.models.DTO;

public class AddAnimal
{
    [Required]
    [MinLength(3)]
    [MaxLength(200)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string? Description { get; set; }
}