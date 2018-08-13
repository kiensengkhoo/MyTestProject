FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 63919
EXPOSE 44302

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY Docker.Web/Docker.Web.csproj Docker.Web/
RUN dotnet restore Docker.Web/Docker.Web.csproj
COPY . .
WORKDIR /src/Docker.Web
RUN dotnet build Docker.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Docker.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Docker.Web.dll"]
