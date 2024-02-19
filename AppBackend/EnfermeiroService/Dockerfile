#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# WORKDIR /app
# EXPOSE 80
# EXPOSE 443

# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /src
# COPY ["EnfermeiroService/EnfermeiroService.csproj", "EnfermeiroService/"]
# RUN dotnet restore "EnfermeiroService/EnfermeiroService.csproj"
# COPY . .
# WORKDIR "/src/EnfermeiroService"
# RUN dotnet build "EnfermeiroService.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "EnfermeiroService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "EnfermeiroService.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /EnfermeiroService

COPY . ./

RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /EnfermeiroService
COPY --from=build-env /EnfermeiroService/out .
ENTRYPOINT ["dotnet", "EnfermeiroService.dll"]