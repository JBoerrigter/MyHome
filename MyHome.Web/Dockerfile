FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443	

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MyHome.Web/MyHome.Web.csproj", "MyHome.Web/"]
RUN dotnet restore "MyHome.Web/MyHome.Web.csproj"
COPY . .
WORKDIR "/src/MyHome.Web"
RUN dotnet build "MyHome.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyHome.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyHome.Web.dll"]