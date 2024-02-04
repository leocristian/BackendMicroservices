#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /src

COPY ConsultaService.csproj . 
RUN dotnet restore
COPY . .

RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0 
WORKDIR /app 
COPY --from=base /app . 
ENTRYPOINT ["dotnet", "ConsultaService.dll"] 