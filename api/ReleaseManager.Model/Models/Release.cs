using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ReleaseManager.Model.Models;

[Index(nameof(Name), IsUnique = true)]
public class Release
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }
}
