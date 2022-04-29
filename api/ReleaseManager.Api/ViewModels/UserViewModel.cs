using ReleaseManager.Model.Enums;
using ReleaseManager.Model.Models;

namespace ReleaseManager.Api.ViewModels;

public class UserViewModel
{
    public string Uid { get; set; } = string.Empty;

    public Roles Role { get; set; }

    public static UserViewModel FromModel(User user)
    {
        return new UserViewModel
        {
            Uid = user.Uid,
            Role = user.Role,
        };
    }
}