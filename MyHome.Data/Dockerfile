#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MyHome.Data/MyHome.Data.csproj", "MyHome.Data/"]
RUN dotnet restore "MyHome.Data/MyHome.Data.csproj"
COPY . .
WORKDIR "/src/MyHome.Data"
RUN dotnet build "MyHome.Data.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyHome.Data.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyHome.Data.dll"]