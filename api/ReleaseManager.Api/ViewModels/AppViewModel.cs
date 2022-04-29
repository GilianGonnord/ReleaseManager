using ReleaseManager.Model.Models;
using ReleaseManager.Model.Models.Enums;
using System.Text.Json.Serialization;

namespace ReleaseManager.Api.ViewModels;

public class AppViewModel
{
    public int? Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public GitProviders GitProvider { get; set; }

    public string? GithubUsername { get; set; }

    public string? GithubProject { get; set; }

    public string? GithubAccessToken { get; set; }

    public string? GitlabOwner { get; set; }

    public string? GitlabProject { get; set; }

    public string? RepoUrl { get; set; }

    public static AppViewModel FromModel(App model)
    {
        return new AppViewModel
        {
            GithubAccessToken = model.GithubCredential?.AccessToken,
            GithubUsername = model.GithubCredential?.Username,
            GithubProject = model.GithubCredential?.Project,
            GitlabOwner = model.GitlabCredential?.Owner,
            GitlabProject = model.GitlabCredential?.Project,
            GitProvider = model.GitProvider,
            Id = model.Id,
            Name = model.Name,
            RepoUrl = model.RepoUrl,
        };
    }

    public App ToModel()
    {
        var app =  new App
        {
            GitProvider = GitProvider,
            Name = Name,
            RepoUrl = RepoUrl
        };

        switch (GitProvider)
        {
            case GitProviders.None:
                app.RepoUrl = RepoUrl;
                break;
            case GitProviders.GitHub:
                if(!string.IsNullOrEmpty(GithubAccessToken) && !string.IsNullOrEmpty(GithubProject) && !string.IsNullOrEmpty(GithubUsername))
                    app.GithubCredential = new GithubCredential { AccessToken = GithubAccessToken, Project = GithubProject, Username = GithubUsername };
                break;
            case GitProviders.GitLab:
                if (!string.IsNullOrEmpty(GitlabOwner) && !string.IsNullOrEmpty(GitlabProject))
                    app.GitlabCredential = new GitlabCredential { Project = GitlabProject, Owner = GitlabOwner };
                break;
        }

        return app;
    }
}
