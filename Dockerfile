# defines the APP_UID
ARG APP_UID=1000

# defines a build argument with the default value of Release
ARG BUILD_CONFIGURATION=Release


########################
#      BASE STAGE      #
########################

# uses the ASP.NET runtime image for .NET 9.0,
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

# creates a non-root user with the specified UID
RUN useradd -u $APP_UID appuser

# sets the user for the container
USER appuser

# sets the working directory for the application inside the container
WORKDIR /app

# exposes ports 8080 and 8081 for external communication
EXPOSE 8080
EXPOSE 8081


########################
#      BUILD STAGE     #
########################

# uses the SDK image for .NET 9.0
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# sets the working directory where the source code will be placed in the container
WORKDIR /src

# copies the moneytale-server.csproj file into /src
COPY ["moneytale-server.csproj", "."]

# restores the NuGet packages required for the project
RUN dotnet restore "./moneytale-server.csproj"

# copies the rest of the application code into the container
COPY . .

# sets the working directory to where the project will be built
WORKDIR "/src/."

# builds the project with the specified configuration and outputs the build artifacts to /app/build
RUN dotnet build "./moneytale-server.csproj" -c $BUILD_CONFIGURATION -o /app/build


########################
#      PUBLISH STAGE   #
########################

# uses the previous build stage
FROM build AS publish

# compiles the app and prepares it for deployment (and prevents the creation of a platform-specific executable file)
RUN dotnet publish "./moneytale-server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


########################
#      FINAL STAGE     #
########################

# uses the base stage
FROM base AS final

# stes where the published application files will be copied
WORKDIR /app

# copies the published files from the publish stage
COPY --from=publish /app/publish .

# sets the entry point for the container
ENTRYPOINT ["dotnet", "moneytale-server.dll"]