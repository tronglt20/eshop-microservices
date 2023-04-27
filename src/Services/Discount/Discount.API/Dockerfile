#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Discount/Discount.API/Discount.API.csproj", "Services/Discount/Discount.API/"]
COPY ["Services/Discount/Discount.Domain/Discount.Domain.csproj", "Services/Discount/Discount.Domain/"]
COPY ["Shared.Domain/Shared.Domain.csproj", "Shared.Domain/"]
COPY ["Services/Discount/Discount.Infrastructure/Discount.Infrastructure.csproj", "Services/Discount/Discount.Infrastructure/"]
COPY ["Shared.Infrastructure/Shared.Infrastructure.csproj", "Shared.Infrastructure/"]
RUN dotnet restore "Services/Discount/Discount.API/Discount.API.csproj"
COPY . .
WORKDIR "/src/Services/Discount/Discount.API"
RUN dotnet build "Discount.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.API.dll"]
