#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["APIGateways/eshop.Aggregator/eshop.Aggregator.csproj", "APIGateways/eshop.Aggregator/"]
RUN dotnet restore "APIGateways/eshop.Aggregator/eshop.Aggregator.csproj"
COPY . .
WORKDIR "/src/APIGateways/eshop.Aggregator"
RUN dotnet build "eshop.Aggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eshop.Aggregator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eshop.Aggregator.dll"]
