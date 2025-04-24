# build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy
COPY ./src ./src
COPY ./tests ./tests
COPY ./PPSRRegistrations.sln ./

# Restore
RUN dotnet restore PPSRRegistrations.sln

# Publish
RUN dotnet publish ./src/PPSRRegistrations.API/PPSRRegistrations.API.csproj -c Release -o /app/publish

# Runtime
EXPOSE 5001
#ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:5001
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "PPSRRegistrations.API.dll"]
