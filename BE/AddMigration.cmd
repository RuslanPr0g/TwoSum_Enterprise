cd ../src
dotnet ef migrations add %1 --project TwoSum.Persistence/TwoSum.Persistence.csproj --startup-project TwoSum_Enterprise/TwoSum_Enterprise.csproj
cd ../scripts