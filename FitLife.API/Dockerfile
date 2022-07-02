#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FitLife.API/FitLife.API.csproj", "FitLife.API/"]
COPY ["FitLife.Infrastructure/FitLife.Infrastructure.csproj", "FitLife.Infrastructure/"]
COPY ["FitLife.DB/FitLife.DB.csproj", "FitLife.DB/"]
COPY ["FitLife.Contracts/FitLife.Contracts.csproj", "FitLife.Contracts/"]
COPY ["FitLife.Shared.Infrastucture/FitLife.Shared.Infrastructure.csproj", "FitLife.Shared.Infrastucture/"]
RUN dotnet restore "FitLife.API/FitLife.API.csproj"
COPY . .
WORKDIR "/src/FitLife.API"
RUN dotnet build "FitLife.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FitLife.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FitLife.API.dll"]