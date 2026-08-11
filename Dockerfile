FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj files
COPY ["Horizons.Data.Common/Horizons.Data.Common.csproj", "Horizons.Data.Common/"]
COPY ["Horizons.Data.Models/Horizons.Data.Models.csproj", "Horizons.Data.Models/"]
COPY ["Horizons.Data/Horizons.Data.csproj", "Horizons.Data/"]
COPY ["Horizons.Services.Common/Horizons.Services.Common.csproj", "Horizons.Services.Common/"]
COPY ["Horizons.Services.Core/Horizons.Services.Core.csproj", "Horizons.Services.Core/"]
COPY ["Horizons.Web.Infrastructure/Horizons.Web.Infrastructure.csproj", "Horizons.Web.Infrastructure/"]
COPY ["Horizons.Web/Horizons.Web.csproj", "Horizons.Web/"]
COPY ["Horizons.API/Horizons.API.Web.csproj", "Horizons.API/"]

# Restore
RUN dotnet restore "Horizons.Web/Horizons.Web.csproj"

# Copy everything else
COPY . .

# Publish
RUN dotnet publish "Horizons.Web/Horizons.Web.csproj" -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Horizons.Web.dll"]