FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar SOMENTE os csproj necessários
COPY src/ApplicantTracking.Api/ApplicantTracking.Api.csproj src/ApplicantTracking.Api/
COPY src/ApplicantTracking.Application/ApplicantTracking.Application.csproj src/ApplicantTracking.Application/
COPY src/ApplicantTracking.Domain/ApplicantTracking.Domain.csproj src/ApplicantTracking.Domain/
COPY src/ApplicantTracking.Infrastructure/ApplicantTracking.Infrastructure.csproj src/ApplicantTracking.Infrastructure/

# Restore 
RUN dotnet restore src/ApplicantTracking.Api/ApplicantTracking.Api.csproj

# Copiar o restante do código
COPY . .

# Build / Publish
WORKDIR /src/src/ApplicantTracking.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
EXPOSE 80

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ApplicantTracking.Api.dll"]
