# Dockerfile para el backend .NET Core
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5108

# Official .NET SDK base image for the build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG APP_NAME=EIC_Back

WORKDIR /app
COPY . .
RUN dotnet tool install --global dotnet-ef --version 7.0.11
ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /app
RUN dotnet restore

ENV DOCKER_BUILD=true
# Build the application
RUN dotnet publish -c Release -r linux-x64 -o out
# Build a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
ARG APP_NAME=EIC_Back
ENV ENV_APP_NAME=$APP_NAME

WORKDIR /app
COPY --from=build /app/out/ ./

ENV ASPNETCORE_URLS=http://+:5108;

# Expose port
  
EXPOSE 5108
CMD dotnet "./$ENV_APP_NAME.dll"
