using ReleaseManager.Model.Models;

namespace ReleaseManager.Api.GitPilot;

public interface IGitPilot
{
    public Task CloneRepo(Config config, App app);
    void DeleteRepo(Config config, App app);
    List<string> GetOngoingReleases(Config config, App app);
}
