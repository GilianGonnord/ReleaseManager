using System.ComponentModel.DataAnnotations;
using ReleaseManager.Model.Enums;

namespace ReleaseManager.Model.Models;

public class User
{
    [Required]
    [Key]
    public string Uid { get; set; } = string.Empty;

    [Required]
    public Roles Role { get; set; }
}