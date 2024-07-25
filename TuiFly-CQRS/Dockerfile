# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY src/TuiFly.Application/TuiFly.Application.csproj ./src/TuiFly.Application/
COPY src/TuiFly.Domain/TuiFly.Domain.csproj ./src/TuiFly.Domain/
COPY src/TuiFly.Domain.Core/TuiFly.Domain.Core.csproj ./src/TuiFly.Domain.Core/
COPY src/TuiFly.Infra.CrossCutting.Bus/TuiFly.Infra.CrossCutting.Bus.csproj ./src/TuiFly.Infra.CrossCutting.Bus/
COPY src/TuiFly.Infra.CrossCutting.IoC/TuiFly.Infra.CrossCutting.IoC.csproj ./src/TuiFly.Infra.CrossCutting.IoC/
COPY src/TuiFly.Infra.Data/TuiFly.Infra.Data.csproj ./src/TuiFly.Infra.Data/
COPY src/TuiFly.Services.Api/TuiFly.Api.csproj ./src/TuiFly.Services.Api/

# .NET Restore
RUN dotnet restore ./src/TuiFly.Services.Api/TuiFly.Api.csproj

# Copy full source code and build
COPY src ./src
WORKDIR /app/src/TuiFly.Services.Api
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Production

# Install required packages for globalization support
RUN apt-get update && apt-get install -y --no-install-recommends \
    libc6-dev \
    libicu-dev \
    && rm -rf /var/lib/apt/lists/*

# Copy published binaries
COPY --from=build /app/publish .

# Expose ports
EXPOSE 80
EXPOSE 443

# Start the application
ENTRYPOINT ["dotnet", "TuiFly.Api.dll"]
