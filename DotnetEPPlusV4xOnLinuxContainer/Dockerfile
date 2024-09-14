# Use the .NET SDK as the build environment
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the solution file and the project files
COPY ../DotnetEPPlusV4xOnLinuxContainer.sln ./
COPY ./DotnetEPPlusV4xOnLinuxContainer/DotnetEPPlusV4xOnLinuxContainer.csproj ./DotnetEPPlusV4xOnLinuxContainer/

# Restore the dependencies
RUN dotnet restore

# Copy the rest of the application
COPY ./DotnetEPPlusV4xOnLinuxContainer ./DotnetEPPlusV4xOnLinuxContainer/

WORKDIR /src/DotnetEPPlusV4xOnLinuxContainer
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app

# Use the runtime image for the final container
FROM mcr.microsoft.com/dotnet/runtime:6.0 AS final

# Install dependencies for System.Drawing.Common (libgdiplus)
RUN apt-get update && \
    apt-get install -y libgdiplus && \
    apt-get clean && \
    ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll

WORKDIR /app
COPY --from=build /app .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "DotnetEPPlusV4xOnLinuxContainer.dll"]
