FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/test/SnowLeopard.WebApi/SnowLeopard.WebApi.csproj src/test/SnowLeopard.WebApi/
RUN dotnet restore src/test/SnowLeopard.WebApi/SnowLeopard.WebApi.csproj
COPY . .
WORKDIR /src/src/test/SnowLeopard.WebApi
RUN dotnet build SnowLeopard.WebApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SnowLeopard.WebApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SnowLeopard.WebApi.dll"]
