# Dockerfile para .NET 10 (preview)
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /app

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:${PORT:-8080}
EXPOSE 8080

ENTRYPOINT ["dotnet", "Frameworks_dev_web_I.dll"]
