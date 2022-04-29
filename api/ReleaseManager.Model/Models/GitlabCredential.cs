namespace ReleaseManager.Model.Models
{
    public class GitlabCredential
    {
        public int Id { get; set; }

        public string Owner { get; set; } = string.Empty;

        public string Project { get; set; } = string.Empty;
    }
}
