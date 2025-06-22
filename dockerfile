# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copia csproj e restaura dependências
COPY *.csproj ./
RUN dotnet restore

# Copia o restante e compila
COPY . ./
RUN dotnet publish -c Release -o out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY ./out ./

# Expõe a porta padrão
EXPOSE 80

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "AuthExample.dll"]
