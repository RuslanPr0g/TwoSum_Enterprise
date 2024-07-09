FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_URLS=https://+:443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TwoSum_Enterprise/TwoSum_Enterprise.csproj", "TwoSum_Enterprise/"]
COPY ["TwoSum.Application/TwoSum.Application.csproj", "TwoSum.Application/"]
COPY ["Enterprise.Application/Enterprise.Application.csproj", "Enterprise.Application/"]
COPY ["TwoSum.Domain/TwoSum.Domain.csproj", "TwoSum.Domain/"]
COPY ["Enterprise.Domain/Enterprise.Domain.csproj", "Enterprise.Domain/"]
COPY ["TwoSum.Messaging/TwoSum.Messaging.csproj", "TwoSum.Messaging/"]
COPY ["TwoSum.Persistence/TwoSum.Persistence.csproj", "TwoSum.Persistence/"]
COPY ["Enterprise.Persistence/Enterprise.Persistence.csproj", "Enterprise.Persistence/"]
COPY ["TwoSum.Quartz/TwoSum.Quartz.csproj", "TwoSum.Quartz/"]
COPY ["Enterprise.Quartz/Enterprise.Quartz.csproj", "Enterprise.Quartz/"]
RUN dotnet restore "./TwoSum_Enterprise/TwoSum_Enterprise.csproj"
COPY . .
WORKDIR "/src/TwoSum_Enterprise"
RUN dotnet build "./TwoSum_Enterprise.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TwoSum_Enterprise.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TwoSum_Enterprise.dll"]
