#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SP.Service/SP.Service.API/SP.Service.API.csproj", "SP.Service/SP.Service.API/"]
COPY ["SP.Service/SP.Service.BusinessLayer/SP.Service.BusinessLayer.csproj", "SP.Service/SP.Service.BusinessLayer/"]
COPY ["SP.Service/SP.Service.DataAccessLayer/SP.Service.DataAccessLayer.csproj", "SP.Service/SP.Service.DataAccessLayer/"]
RUN dotnet restore "SP.Service/SP.Service.API/SP.Service.API.csproj"
COPY . .
WORKDIR "/src/SP.Service/SP.Service.API"
RUN dotnet build "SP.Service.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SP.Service.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SP.Service.API.dll"]