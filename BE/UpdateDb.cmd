cd ../src
dotnet ef database update -p TwoSum.Persistence/TwoSum.Persistence.csproj -s TwoSum_Enterprise/TwoSum_Enterprise.csproj -c TwoSumContext
cd ../scripts