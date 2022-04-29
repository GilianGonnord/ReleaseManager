using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ReleaseManager.Model.Models;

namespace ReleaseManager.Api.ViewModels;

public class ReleaseViewModel
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [Required]
    [MinLength(3)]
    [JsonPropertyName("version_number")]
    public string? VersionNumber { get; set; }

    [JsonPropertyName("date_created")]
    public DateTime? DateCreated { get; set; }

    public static ReleaseViewModel FromModel(Release release)
    {
        return new ReleaseViewModel
        {
            Id = release.Id,
            VersionNumber = release.VersionNumber,
            DateCreated = release.DateCreated
        };
    }

    public Release ToModel()
    {
        var release = new Release { VersionNumber = VersionNumber };

        if (Id.HasValue)
            release.Id = Id.Value;

        return release;
    }
}