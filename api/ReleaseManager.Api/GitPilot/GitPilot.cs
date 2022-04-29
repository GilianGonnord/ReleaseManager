using ReleaseManager.Api.Hubs;
using ReleaseManager.Model.Models;
using System.Text.RegularExpressions;

namespace ReleaseManager.Api.GitPilot;

public class GitPilot : IGitPilot
{
    private readonly GitLogNotifyService _gitLogNotifyService;

    public GitPilot(GitLogNotifyService gitLogNotifyService)
    {
        _gitLogNotifyService = gitLogNotifyService;
    }

    public async Task CloneRepo(Config config, App app)
    {
        await _gitLogNotifyService.SendNotificationAsync("CloneRepo");

        var projectDir = new DirectoryInfo(GetProjectPath(config, app));

        DeleteDirIfExist(projectDir);

        projectDir.Create();

        string repoUrl;

        if (app.GitHubCredentialId.HasValue && app.GithubCredential != null)
        {
            var credential = app.GithubCredential;
            //https://<token>@github.com/owner/repo.git
            repoUrl = $"https://{credential.AccessToken}@github.com/{credential.Username}/{credential.Project}.git";
        } else if (app.GitlabCredentialId.HasValue && app.GitlabCredential != null)
        {
            var credential = app.GitlabCredential;
            repoUrl = $"git@gitlab.com:{credential.Owner}/{credential.Project}.git";
        } else if (!string.IsNullOrEmpty(app.RepoUrl))
        {
            repoUrl = app.RepoUrl;
        } else
        {
            throw new ArgumentNullException(nameof(app.RepoUrl));
        }

        var command = $"clone {repoUrl} .";

        var gitProcessor = new GitProccessor();

        async void eventHandler(string data)
        {
            await _gitLogNotifyService.SendNotificationAsync(data);
        }

        gitProcessor.RunCommand(config.GitExePath, projectDir.FullName, command, eventHandler);
    }

    public void DeleteRepo(Config config, App app)
    {
        var projectDir = new DirectoryInfo(GetProjectPath(config, app));

        DeleteDirIfExist(projectDir);
    }

    private static string GetProjectPath(Config config, App app) => Path.Combine(config.RootProjectRepository, app.Id.ToString());

    private void SetAttributesNormal(DirectoryInfo dir)
    {
        foreach (var subDir in dir.GetDirectories())
            SetAttributesNormal(subDir);

        foreach (var file in dir.GetFiles())
        {
            file.Attributes = FileAttributes.Normal;
        }
    }

    private void DeleteDirIfExist(DirectoryInfo dir)
    {
        if (dir.Exists)
        {
            SetAttributesNormal(dir);
            dir.Delete(true);
        }
    }

    public List<string> GetOngoingReleases(Config config, App app)
    {
        var projectDir = new DirectoryInfo(GetProjectPath(config, app));

        var gitProcessor = new GitProccessor();

        var output = gitProcessor.RunCommand(config.GitExePath, projectDir.FullName, "tag");

        var tags = output.Split("\r\n");

        var rels = tags.Where(t => Regex.IsMatch(t, @"\d.\d.\d")).ToList();
        var intermediateRels = tags.Where(t => Regex.IsMatch(t, @"\d.\d.\d[a-z]")).ToList();

        var ongoingRels = intermediateRels
            .Where(intermediateRel => !rels.Contains(intermediateRel[..^1]))
            .ToList();

        ongoingRels.Sort((a, b) => b.CompareTo(a));

        var lastTagByRelease = ongoingRels.GroupBy(t => t[..5]).Select(n => n.First()).ToList();

        return lastTagByRelease;
    }
}
