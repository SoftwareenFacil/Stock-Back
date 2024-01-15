# Official .NET SDK base image for the build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /app

COPY . .

RUN dotnet restore

# Build the application
RUN dotnet publish -c Release -r linux-x64 -o out

# Build a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app/

COPY --from=build /app/out/ ./

ENV ASPNETCORE_URLS=http://+:5108;

# Expose port
EXPOSE 5108

# Start backend application
ENTRYPOINT ["dotnet", "Stock-Back.dll"]

