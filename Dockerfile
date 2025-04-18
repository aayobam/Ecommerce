# Use the official .NET 8 SDK image for building the appFROM mcr.microsoft.com/dotnet/sdk:8.0 AS buildWORKDIR /src# Copy the .sln file and all projects into the containerCOPY *.sln .COPY Api/Api.csproj Api/COPY Application/Application.csproj Application/COPY Domain/Domain.csproj Domain/COPY Infrastructure/Infrastructure.csproj Infrastructure/COPY Persistence/Persistence.csproj Persistence/# Restore dependencies for all projectsRUN dotnet restore# Copy the entire solution into the containerCOPY . .# Build the solution (Release configuration by default)RUN dotnet build -c Release --no-restore# Publish the API project to the /app directoryRUN dotnet publish Api/Api.csproj -c Release -o /app --no-restore# Use a lightweight runtime image for running the appFROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtimeWORKDIR /app# Copy the published app from the build stageCOPY --from=build /app .# Set the entry point to the APIENTRYPOINT ["dotnet", "Api.dll"]# Expose the default port (e.g., 5000)EXPOSE 5000
1
1
1
1
1
1
1
1
1
1
1
1
1
1
1
