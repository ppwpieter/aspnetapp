# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["aspnetapp/aspnetapp.csproj", "aspnetapp/"]
RUN dotnet restore "./aspnetapp/aspnetapp.csproj"
COPY . .
WORKDIR "/src/aspnetapp"
RUN dotnet build "./aspnetapp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./aspnetapp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
EXPOSE 8080
EXPOSE 8081
WORKDIR /app
COPY --link --from=publish /app/publish .
USER $APP_UID
ENTRYPOINT ["dotnet", "aspnetapp.dll"]