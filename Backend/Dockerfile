# --------------------------------------------
# Stage 1: Base runtime image
# --------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 7105

# --------------------------------------------
# Stage 2: Build environment
# --------------------------------------------
USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src

# Copy project files
COPY ["Backend/BikeShopManager.AppCore/BikeShopManager.AppCore.csproj", "Backend/BikeShopManager.AppCore/"]
COPY ["Backend/BikeShopManager.Domain/BikeShopManager.Domain.csproj", "Backend/BikeShopManager.Domain/"]
COPY ["Backend/BikeShopManager.Infrastructure/BikeShopManager.Infrastructure.csproj", "Backend/BikeShopManager.Infrastructure/"]
COPY ["Backend/BikeShopManager.WebApi/BikeShopManager.WebApi.csproj", "Backend/BikeShopManager.WebApi/"]
# Restore dependencies
RUN dotnet restore "Backend/BikeShopManager.WebApi/BikeShopManager.WebApi.csproj"
# Copy all source code and build
COPY Backend/ Backend/
WORKDIR "/src/Backend/BikeShopManager.WebApi"
RUN dotnet build "BikeShopManager.WebApi.csproj" -c $configuration -o /app/build

# --------------------------------------------
# Stage 3: Publish
# --------------------------------------------
FROM build AS publish
ARG configuration=Release
RUN dotnet publish "BikeShopManager.WebApi.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

# --------------------------------------------
# Stage : Dev (DevContainer)
# --------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS dev
WORKDIR /workspaces

# --------------------------------------------
# Stage 4: Final image
# --------------------------------------------
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BikeShopManager.WebApi.dll"]
