#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build # Extrair a imagem do .NET (pull)
WORKDIR /src # Definição do diretório na imagem

COPY ConsultaService.csproj . 
RUN dotnet restore # Baixar as dependências do aplicativo contidas no arquivo .csproj
COPY . .

RUN dotnet publish -c release -o /app # Compila a aplicação, verifica as dependências e publica os resultados em um novo diretório

FROM mcr.microsoft.com/dotnet/aspnet:6.0 # Extrai a imagem do aspnet 6.0
WORKDIR /app # Definição do diretório da imagem
COPY --from=build /app . # Copia tudo que foi feito em build e cola no novo diretório "app"
ENTRYPOINT ["dotnet", "ConsultaService.dll"] # Especificação do comando para executar a dll gerada na compuilação
