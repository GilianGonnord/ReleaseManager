namespace ReleaseManager.Model.Models;

public class Config
{
    public int Id { get; set; }

    public string GitExePath { get; set; } = string.Empty;

    public string RootProjectRepository { get; set; } = string.Empty;
}
