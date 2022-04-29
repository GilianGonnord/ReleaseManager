namespace ReleaseManager.Model.Enums;

[Flags]
public enum Roles
{
    SuperAdmin = 0,
    Admin = 1 << 0,
    Manager = 1 << 1,
    User = 1 << 2
}