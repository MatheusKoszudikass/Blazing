# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Blazing.Api/Blazing.Api.csproj", "Blazing.Api/"]
COPY ["Blazing.Application/Blazing.Application.csproj", "Blazing.Application/"]
COPY ["Blazing.Domain/Blazing.Domain.csproj", "Blazing.Domain/"]
COPY ["Blazing.infrastructure/Blazing.Ecommerce/Blazing.Ecommerce.csproj", "Blazing.infrastructure/Blazing.Ecommerce/"]
COPY ["Blazing.infrastructure/Blazing.Identity/Blazing.Identity.csproj", "Blazing.infrastructure/Blazing.Identity/"]
RUN dotnet restore "./Blazing.Api/Blazing.Api.csproj"
COPY . .
WORKDIR "/src/Blazing.Api"
RUN dotnet build "./Blazing.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Blazing.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blazing.Api.dll"]