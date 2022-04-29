namespace ReleaseManager.Model.Models.Enums;

public enum GitProviders
{
    None = 0,
    GitHub = 1 << 0,
    GitLab = 1 << 1,
}
