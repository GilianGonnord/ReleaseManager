using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ReleaseManager.Model.Models;

[Index(nameof(VersionNumber), IsUnique = true)]
public class Release
{
    public int Id { get; set; }

    [Required]
    public string? VersionNumber { get; set; }

    public DateTime DateCreated { get; set; }
}
