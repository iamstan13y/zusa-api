FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ZUSA.API/ZUSA.API.csproj", "ZUSA.API/"]
RUN dotnet restore "ZUSA.API/ZUSA.API.csproj"
COPY . .
WORKDIR "/src/ZUSA.API"
RUN dotnet build "ZUSA.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ZUSA.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZUSA.API.dll"]