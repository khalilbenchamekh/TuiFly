# .NET Core SDK
FROM mcr.microsoft.com/dotnet/core/sdk:6.0-alpine AS build

# Sets the working directory
WORKDIR /app

# Copy Projects
#COPY *.sln .
COPY Src/TuiFly.Application/TuiFly.Application.csproj ./Src/TuiFly.Application/
COPY Src/TuiFly.Domain/TuiFly.Domain.csproj ./Src/TuiFly.Domain/
COPY Src/TuiFly.Domain.Core/TuiFly.Domain.Core.csproj ./Src/TuiFly.Domain.Core/
COPY Src/TuiFly.Infra.CrossCutting.Bus/TuiFly.Infra.CrossCutting.Bus.csproj ./Src/TuiFly.Infra.CrossCutting.Bus/
COPY Src/TuiFly.Infra.CrossCutting.IoC/TuiFly.Infra.CrossCutting.IoC.csproj ./Src/TuiFly.Infra.CrossCutting.IoC/
COPY Src/TuiFly.Infra.Data/TuiFly.Infra.Data.csproj ./Src/TuiFly.Infra.Data/
COPY Src/TuiFly.Api/TuiFly.Api.csproj ./Src/TuiFly.Api/

# .NET Core Restore
RUN dotnet restore ./Src/TuiFly.Api/TuiFly.Api.csproj

# Copy All Files
COPY Src ./Src

# .NET Core Build and Publish
RUN dotnet publish ./Src/TuiFly.Api/TuiFly.Api.csproj -c Release -o /publish

# ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:6.0-alpine AS runtime
WORKDIR /app
COPY --from=build /publish ./

# Expose ports
EXPOSE 80
EXPOSE 443

# Setup your variables before running.
ARG MyEnv
ENV ASPNETCORE_ENVIRONMENT $MyEnv

ENTRYPOINT ["dotnet", "TuiFly.Api.dll"]
