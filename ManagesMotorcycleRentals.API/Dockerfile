# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ManagesMotorcycleRentals.sln ./
COPY ManagesMotorcycleRentals.API/*.csproj ManagesMotorcycleRentals.API/
COPY ManagesMotorcycleRentals.Application/*.csproj ManagesMotorcycleRentals.Application/
COPY ManagesMotorcycleRentals.Domain/*.csproj ManagesMotorcycleRentals.Domain/
COPY ManagesMotorcycleRentals.Infrastructure/*.csproj ManagesMotorcycleRentals.Infrastructure/
COPY ManagesMotorcycleRentals.Services/*.csproj ManagesMotorcycleRentals.Services/
COPY ManagesMotorcycleRentals.DTOs/*.csproj ManagesMotorcycleRentals.DTOs/
COPY ManagesMotorcycleRentals.Worker/* ManagesMotorcycleRentals.Worker/
COPY ManagesMotorcycleRentals.Tests/* ManagesMotorcycleRentals.Tests/


# Restaura dependências
RUN dotnet restore "ManagesMotorcycleRentals.sln"

# Copia tudo
COPY . .

# Publica a API
RUN dotnet publish "ManagesMotorcycleRentals.API/ManagesMotorcycleRentals.API.csproj" -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ManagesMotorcycleRentals.API.dll"]
