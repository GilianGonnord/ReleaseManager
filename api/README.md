dotnet user-secrets set "ReleaseManager:FirebaseConfidential" "[FirebaseAdminSdkJson]"

dotnet build
dotnet test
dotnet ef migrations add -p .\ReleaseManager.Model\  -s .\ReleaseManager.Api\ MigrationName
dotnet ef database update -p .\ReleaseManager.Model\ -s .\ReleaseManager.Api\ 