# NuGet restore
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.sln .
COPY 0.Core/*.csproj 0.Core/
COPY 1.Web/*.csproj 1.Web/
COPY 2.BLL/*.csproj 2.BLL/
COPY 3.DAL/*.csproj 3.DAL/
RUN dotnet restore
COPY . .

# publish
FROM build AS publish
WORKDIR /src/1.Web
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
# ENTRYPOINT ["dotnet", "WebSite.dll"]
# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Web.dll