# -------------------------------
# Etapa 1: build
# -------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de projeto
COPY ["ManagesMotorcycleRentals.Worker/ManagesMotorcycleRentals.Worker.csproj", "ManagesMotorcycleRentals.Worker/"]
COPY ["ManagesMotorcycleRentals.Application/ManagesMotorcycleRentals.Application.csproj", "ManagesMotorcycleRentals.Application/"]
COPY ["ManagesMotorcycleRentals.Domain/ManagesMotorcycleRentals.Domain.csproj", "ManagesMotorcycleRentals.Domain/"]
COPY ["ManagesMotorcycleRentals.Infrastructure/ManagesMotorcycleRentals.Infrastructure.csproj", "ManagesMotorcycleRentals.Infrastructure/"]

# Restaura dependências
RUN dotnet restore "ManagesMotorcycleRentals.Worker/ManagesMotorcycleRentals.Worker.csproj"

# Copia o restante do código
COPY . .

# Compila e publica
WORKDIR "/src/ManagesMotorcycleRentals.Worker"
RUN dotnet publish "ManagesMotorcycleRentals.Worker.csproj" -c Release -o /app/publish

# -------------------------------
# Etapa 2: runtime
# -------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ManagesMotorcycleRentals.Worker.dll"]
