# Official .NET SDK base image for the build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /app

COPY . .

RUN dotnet tool install --global dotnet-ef --version 7.0.11
ENV PATH="$PATH:/root/.dotnet/tools"

# Generate migration script
RUN dotnet ef --project ./Stock-Back.DAL --startup-project ./Stock-Back \
dbcontext script -o ./script.sql


WORKDIR /app
RUN dotnet restore

# Build the application
RUN dotnet publish -c Release -r linux-x64 -o out

# Build a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

COPY --from=build /app/out/ ./
COPY --from=build /app/script.sql /app/script/

ENV ASPNETCORE_URLS=http://+:5108;

# Expose port
EXPOSE 5108

CMD ["dotnet", "./Stock-Back.dll"]




