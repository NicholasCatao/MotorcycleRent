#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RentMotorBike.Api/RentMotorBike.Api.csproj", "RentMotorBike.Api/"]
COPY ["RentMotorBike.Application/RentMotorBike.Application.csproj", "RentMotorBike.Application/"]
COPY ["RentMotorBike.Domain/RentMotorBike.Domain.csproj", "RentMotorBike.Domain/"]
COPY ["RentMotorBike.Infrastructure.CrossCutting/RentMotorBike.Infrastructure.CrossCutting.csproj", "RentMotorBike.Infrastructure.CrossCutting/"]
COPY ["RentMotorBike.Infra/RentMotorBike.Infra.csproj", "RentMotorBike.Infra/"]
RUN dotnet restore "./RentMotorBike.Api/RentMotorBike.Api.csproj"
COPY . .
WORKDIR "/src/RentMotorBike.Api"
RUN dotnet build "./RentMotorBike.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RentMotorBike.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentMotorBike.Api.dll"]