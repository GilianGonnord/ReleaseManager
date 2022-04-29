namespace ReleaseManager.Model.Models
{
    public class GithubCredential
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;

        public string Project { get; set; } = string.Empty;
    }
}
