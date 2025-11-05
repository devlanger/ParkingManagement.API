FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CarAssignment.API/CarAssignment.API.csproj", "CarAssignment.API/"]
COPY ["CarAssignment.Application.CQRS/CarAssignment.Application.CQRS.csproj", "CarAssignment.Application.CQRS/"]
COPY ["CarAssignment.Core/CarAssignment.Core.csproj", "CarAssignment.Core/"]
COPY ["CarAssignment.Infrastructure/CarAssignment.Infrastructure.csproj", "CarAssignment.Infrastructure/"]
COPY ["CarAssignment.Application/CarAssignment.Application.csproj", "CarAssignment.Application/"]
RUN dotnet restore "CarAssignment.API/CarAssignment.API.csproj"
COPY . .
WORKDIR "/src/CarAssignment.API"
RUN dotnet build "CarAssignment.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CarAssignment.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarAssignment.API.dll"]
