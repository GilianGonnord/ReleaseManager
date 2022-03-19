using System.ComponentModel.DataAnnotations;
using ReleaseManager.Model.Models;

namespace ReleaseManager.Api.ViewModels;

public class ReleaseViewModel
{
    public int? Id { get; set; }

    [Required]
    [MinLength(3)]
    public string? Name { get; set; }

    public static ReleaseViewModel FromModel(Release release)
    {
        return new ReleaseViewModel
        {
            Id = release.Id,
            Name = release.Name,
        };
    }

    public Release ToModel()
    {
        var release = new Release { Name = Name };

        if (Id.HasValue)
            release.Id = Id.Value;

        return release;
    }
}