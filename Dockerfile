#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["sport-event/sport-event.csproj", "sport-event/"]
RUN dotnet restore "sport-event/sport-event.csproj"
COPY . .
WORKDIR "/src/sport-event"
RUN dotnet build "sport-event.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sport-event.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sport-event.dll"]