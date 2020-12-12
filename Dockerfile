# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY *.sln .
COPY WebSite/*.csproj WebSite/
RUN dotnet restore
COPY . .

# publish
FROM build AS publish
WORKDIR /src/WebSite
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
# ENTRYPOINT ["dotnet", "WebSite.dll"]
# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet WebSite.dll