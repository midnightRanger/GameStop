FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GameStop/GameStop.csproj", "GameStop/"]
RUN dotnet restore "GameStop/GameStop.csproj"
COPY . .
WORKDIR "/src/GameStop"
RUN dotnet build "GameStop.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameStop.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameStop.dll"]
