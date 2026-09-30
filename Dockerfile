# Stage 1: Base Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Stage 2: SDK Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Programming-Contest-Platform/Programming-Contest-Platform.csproj", "src/Programming-Contest-Platform/"]
RUN dotnet restore "src/Programming-Contest-Platform/Programming-Contest-Platform.csproj"
COPY . .
WORKDIR "/src/src/Programming-Contest-Platform"
RUN dotnet build "Programming-Contest-Platform.csproj" -c Release -o /app/build

# Stage 3: Publish
FROM build AS publish
RUN dotnet publish "Programming-Contest-Platform.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Programming-Contest-Platform.dll"]