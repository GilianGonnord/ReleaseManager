[![Coverage Status](https://coveralls.io/repos/github/GilianGonnord/ReleaseManager/badge.svg?branch=main)](https://coveralls.io/github/GilianGonnord/ReleaseManager?branch=main)

# ReleaseManager

## Api

dotnet build
dotnet test
dotnet ef migrations add -p .\ReleaseManager.Model\  -s .\ReleaseManager.Api\ MigrationName
dotnet ef database update -p .\ReleaseManager.Model\ -s .\ReleaseManager.Api\ 