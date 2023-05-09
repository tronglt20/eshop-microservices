#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["APIGateways/eshop.APIGateway/eshop.APIGateway.csproj", "APIGateways/eshop.APIGateway/"]
RUN dotnet restore "APIGateways/eshop.APIGateway/eshop.APIGateway.csproj"
COPY . .
WORKDIR "/src/APIGateways/eshop.APIGateway"
RUN dotnet build "eshop.APIGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eshop.APIGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eshop.APIGateway.dll"]
