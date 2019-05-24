FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["WebApi/APIPrevisaoTempo.WebApi/APIPrevisaoTempo.WebApi.csproj", "WebApi/APIPrevisaoTempo.WebApi/"]
RUN dotnet restore "WebApi/APIPrevisaoTempo.WebApi/APIPrevisaoTempo.WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi/APIPrevisaoTempo.WebApi"
RUN dotnet build "APIPrevisaoTempo.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "APIPrevisaoTempo.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "APIPrevisaoTempo.WebApi.dll"]