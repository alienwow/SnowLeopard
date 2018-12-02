FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["sample/SnowLeopard.WebApi/SnowLeopard.WebApi.csproj", "sample/SnowLeopard.WebApi/"]
COPY ["src/SnowLeopard/SnowLeopard.csproj", "src/SnowLeopard/"]
COPY ["src/SnowLeopard.Lynx/SnowLeopard.Lynx.csproj", "src/SnowLeopard.Lynx/"]
COPY ["src/SnowLeopard.Model/SnowLeopard.Model.csproj", "src/SnowLeopard.Model/"]
COPY ["src/SnowLeopard.Mongo/SnowLeopard.Mongo.csproj", "src/SnowLeopard.Mongo/"]
COPY ["src/SnowLeopard.Redis/SnowLeopard.Redis.csproj", "src/SnowLeopard.Redis/"]
COPY ["src/SnowLeopard.Caching.Redis/SnowLeopard.Caching.Redis.csproj", "src/SnowLeopard.Caching.Redis/"]
COPY ["src/SnowLeopard.Caching.Abstractions/SnowLeopard.Caching.Abstractions.csproj", "src/SnowLeopard.Caching.Abstractions/"]
RUN dotnet restore "sample/SnowLeopard.WebApi/SnowLeopard.WebApi.csproj"
COPY . .
WORKDIR "/src/sample/SnowLeopard.WebApi"
RUN dotnet build "SnowLeopard.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SnowLeopard.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SnowLeopard.WebApi.dll"]