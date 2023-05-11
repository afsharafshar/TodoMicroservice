#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Src/Services/TodoService.Api/TodoService.Api.csproj", "Src/Services/TodoService.Api/"]
COPY ["Src/BuildingBlocks/EventBus.Common/EventBus.Common.csproj", "Src/BuildingBlocks/EventBus.Common/"]
COPY ["Src/BuildingBlocks/IdentityCommon/IdentityCommon.csproj", "Src/BuildingBlocks/IdentityCommon/"]
COPY ["Src/BuildingBlocks/Serilog.Common/Serilog.Common.csproj", "Src/BuildingBlocks/Serilog.Common/"]
COPY ["Src/BuildingBlocks/Swagger.Common/Swagger.Common.csproj", "Src/BuildingBlocks/Swagger.Common/"]
RUN dotnet restore "Src/Services/TodoService.Api/TodoService.Api.csproj"
COPY . .
WORKDIR "/src/Src/Services/TodoService.Api"
RUN dotnet build "TodoService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoService.Api.dll"]