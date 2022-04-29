using ReleaseManager.Model.Models.Enums;

namespace ReleaseManager.Model.Models;

public class App
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? GitHubCredentialId { get; set; }

    public int? GitlabCredentialId { get; set; }

    public GitProviders GitProvider { get; set; }

    public string? RepoUrl { get; set; }

    public GithubCredential? GithubCredential { get; set; }

    public GitlabCredential? GitlabCredential { get; set; }
}
