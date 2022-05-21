#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FoodDelevering.Api/FoodDelevering.Api.csproj", "FoodDelevering.Api/"]
COPY ["FoodDeleveryApp.Data/FoodDeleveryApp.Data.csproj", "FoodDeleveryApp.Data/"]
RUN dotnet restore "FoodDelevering.Api/FoodDelevering.Api.csproj"
COPY . .
WORKDIR "/src/FoodDelevering.Api"
RUN dotnet build "FoodDelevering.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodDelevering.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodDelevering.Api.dll"]