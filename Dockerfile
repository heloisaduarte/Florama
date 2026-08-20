FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/ src/
RUN dotnet restore src/Florama.Api/Florama.Api.csproj

RUN dotnet publish src/Florama.Api/Florama.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["/bin/sh", "-c", "dotnet Florama.Api.dll --urls http://+:${PORT:-8080}"]